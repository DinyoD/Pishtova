// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  API_URL : 'https://localhost:44329',
  API_HOME_URL: 'http://localhost:4200/profile',
  API_PAY_SUCCESS_URL: 'http://localhost:4200/memberships/success',
  API_PAY_CANCEL_URL: 'http://localhost:4200/memberships/failure',
  CLIENT_URI: 'http://localhost:4200',
  firebaseConfig: null,
  Imgur_Client_ID: '74d88bb9375f249',
  Imgur_Client_Secret: '9122920fb61ad38056966e7d6fa1c74b7997e920',
  Imgur_Url: 'https://api.imgur.com/3/image',
};

