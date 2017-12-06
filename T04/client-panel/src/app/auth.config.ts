import { AuthConfig } from 'angular-oauth2-oidc';
const authorityurl  = 'http://localhost:5000';

export const authConfig: AuthConfig = {

    // Url of the Identity Provider
    issuer: authorityurl,
    loginUrl : authorityurl + '/connect/authorize',
    logoutUrl : authorityurl + '/connect/endsession?id_token={{id_token}}',
    // The SPA's id. The SPA is registerd with this id at the auth-server
    clientId: 'sales',
    // URL of the SPA to redirect the user to after login
    redirectUri: window.location.origin + '/login-callback',
    responseType : 'id_token token',
    scope : 'openid profile email salesapi',
    postLogoutRedirectUri : window.location.origin,
    // URL of the SPA to redirect the user after silent refresh
    silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',

    showDebugInformation: true,
    sessionChecksEnabled: true
}