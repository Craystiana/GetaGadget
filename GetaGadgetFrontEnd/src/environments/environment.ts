// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false
};

export const API_URL = 'https://localhost:44344';
export const LOGIN_URL = '/user/login'; 
export const REGISTER_URL = '/user/register';
export const PRODUCT_EDIT_URL = '/product/edit';
export const PRODUCT_DATA_URL = '/product/data';
export const PRODUCT_DETAIL_URL = '/product/detail?productId=';
export const PRODUCT_DELETE_URL = '/product/delete?productId=';
export const PRODUCT_LIST_URL = '/product/list';
export const ORDER_INDEX_URL = '/order';
export const ORDER_ADD_URL = '/order/add';
export const ORDER_DELETE_URL = '/order/delete';
export const ORDER_HISTORY_URL = '/order/history';
export const ORDER_PLACE_URL = '/order/place';
export const WISHLIST_INDEX_URL = '/wishlist';
export const WISHLIST_ADD_URL = '/wishlist/add';
export const WISHLIST_DELETE_URL = '/wishlist/delete';

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
