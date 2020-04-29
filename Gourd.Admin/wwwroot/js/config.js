//配置信息
var config = {
    authority: "https://ids4.wmowm.com",
    //authority: "http://localhost:5000",
    client_id: "mvc",
    //redirect_uri: "https://admin.wmowm.com/home/callback",
    redirect_uri: "https://localhost:5001/home/callback",
    response_type: "id_token token",
    //post_logout_redirect_uri: "https://admin.wmowm.com/home/web",
    post_logout_redirect_uri: "https://localhost:5001/js/index.html",
    scope: "openid profile CCC" //范围一定要写,不然access_token访问资源会401
};
var mgr = new Oidc.UserManager(config);