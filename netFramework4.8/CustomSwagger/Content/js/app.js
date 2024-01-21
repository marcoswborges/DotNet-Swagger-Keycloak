var keycloak = new Keycloak({
    url: 'http://localhost:8080/',
    realm: 'CustomSwagger',
    clientId: 'swagger'
});
    keycloak.init({ onLoad: 'login-required' });

//function iniKeycloak() {
//    keycloak.init({
//        checkLoginIframe: false
//    }).then(function (authenticated) {
//        debugger
//        if (!authenticated) {
//            debugger
//            keycloak.login();
//        }

//    }).catch(function (e) {
//        debugger
//        keycloak.login();
//    });
//}

//iniKeycloak();