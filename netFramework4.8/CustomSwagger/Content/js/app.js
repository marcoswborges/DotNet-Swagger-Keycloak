var keycloak = new Keycloak({
    url: 'http://localhost:8080/',
    realm: 'CustomSwagger',
    clientId: 'swagger'
});
    keycloak.init({ onLoad: 'login-required' });