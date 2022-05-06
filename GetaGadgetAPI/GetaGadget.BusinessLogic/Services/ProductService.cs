using GetaGadget.Domain.DTO.Generic;
using GetaGadget.Domain.DTO.Product;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GetaGadget.BusinessLogic.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductModel GetProductDetails(int productId)
        {
            var product = _unitOfWork.ProductRepository.Get(productId);

            return new ProductModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Provider = product.Provider.Name,
                DeliveryMethod = product.DeliveryMethod.Name,
                Category = product.Category.Name,
                Photo = ConvertToBase64String(product.Photo)
            };
        }

        public ProductEditModel GetProduct(int productId)
        {
            var product = _unitOfWork.ProductRepository.Get(productId);

            return new ProductEditModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Provider = product.ProviderId,
                DeliveryMethod = product.DeliveryMethodId,
                Category = product.CategoryId,
                Photo = ConvertToBase64String(product.Photo)
            };
        }

        public ProductDataModel GetProductData()
        {
            return new ProductDataModel
            {
                Providers = _unitOfWork.ProviderRepository.GetAll().Select(ct => new GenericModel { Id = ct.ProviderId, Name = ct.Name }),
                DeliveryMethods = _unitOfWork.DeliveryMethodRepository.GetAll().Select(ct => new GenericModel { Id = ct.DeliveryMethodId, Name = ct.Name }),
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(ct => new GenericModel { Id = ct.CategoryId, Name = ct.Name })
            };
        }

        public IEnumerable<ProductModel> GetList(ProductQueryModel model)
        {
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                CreateIndex();
                return Search(model.SearchTerm);
            }

            return _unitOfWork.ProductRepository.GetList(model.SearchTerm, model.ProviderIds, model.DeliveryMethodIds, model.CategoryIds, model.InStock, model.SortById)
                                                .Select(p => new ProductModel
                                                {
                                                    ProductId = p.ProductId,
                                                    Name = p.Name,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    Stock = p.Stock,
                                                    Provider = p.Provider.Name,
                                                    DeliveryMethod = p.DeliveryMethod.Name,
                                                    Category = p.Category.Name,
                                                    Photo = ConvertToBase64String(p.Photo)
                                                });
        }

        public void Add(ProductEditModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ProviderId = model.Provider,
                DeliveryMethodId = model.DeliveryMethod,
                CategoryId = model.Category,
                Photo = Compress(ConvertToByteArray(model.Photo))
            };

            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.SaveChanges();
        }

        public void Edit(ProductEditModel model)
        {
            var product = _unitOfWork.ProductRepository.Get((int)model.ProductId);

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.ProviderId = model.Provider;
            product.DeliveryMethodId = model.DeliveryMethod;
            product.CategoryId = model.Category;

            if (model.Photo != null)
            {
                product.Photo = Compress(ConvertToByteArray(model.Photo));
            }

            _unitOfWork.SaveChanges();
        }

        public void Delete(int productId)
        {
            _unitOfWork.ProductRepository.Remove(_unitOfWork.ProductRepository.Get(productId));
            _unitOfWork.SaveChanges();
        }

        public byte[] ConvertToByteArray(string img)
        {
            return string.IsNullOrEmpty(img) ? null : Convert.FromBase64String(img);
        }

        public string ConvertToBase64String(byte[] byteArray)
        {
            return byteArray == null ? null : Convert.ToBase64String(Decompress(byteArray));
        }

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }

            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }

            return output.ToArray();
        }

        public IEnumerable<string> SearchProductNames(string query)
        {
            return _unitOfWork.ProductRepository.GetList(query, null, null, null, null, null).Select(p => p.Name);
        }

        public void CreateIndex()
        {
            var dir = FSDirectory.Open(new DirectoryInfo(@"D:/test_lucene"));
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var products = _unitOfWork.ProductRepository.GetList(null, null, null, null, null, null);

            using (var writer = new IndexWriter(dir, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var product in products)
                {
                    var doc = new Document();
                    doc.Add(new Field("Id", product.ProductId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("Name", product.Name, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Description", product.Description, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Provider", product.Provider.Name, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Price", product.Price.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Image", ConvertToBase64String(product.Photo), Field.Store.YES, Field.Index.ANALYZED));

                    writer.AddDocument(doc);
                    writer.Optimize();
                    writer.Commit();
                }
            }
        }

        public IEnumerable<ProductModel> Search(string text)
        {
            var directory = FSDirectory.Open(new DirectoryInfo(@"D:/test_lucene"));
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new[] { "Name", "Description", "Price", "Provider", "Image", }, analyzer);
            if (text is null)
                return new List<ProductModel>();
            Query query = parser.Parse(text);
            var searcher = new IndexSearcher(directory, true);
            TopDocs topDocs = searcher.Search(query, 10);

            var productList = new List<ProductModel>();
            int results = topDocs.ScoreDocs.Length;

            for (int i = 0; i < results; i++)
            {
                ScoreDoc scoreDoc = topDocs.ScoreDocs[i];
                float score = scoreDoc.Score;
                int docId = scoreDoc.Doc;
                Document doc = searcher.Doc(docId);

                if (!productList.Any(x => x.ProductId == Int32.Parse(doc.Get("Id"))))
                {
                    productList.Add(new ProductModel
                    {
                        ProductId = Int32.Parse(doc.Get("Id")),
                        Name = doc.Get("Name"),
                        Provider = doc.Get("Provider"),
                        Description = doc.Get("Description"),
                        Photo = doc.Get("Image"),
                        Price = float.Parse(doc.Get("Price"))
                    });
                }
            }

            return productList;
        }
    }
}
