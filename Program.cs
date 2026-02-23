using ASP_NET_03._Razor_Pages_Product_Site.Data;
using ASP_NET_03._Razor_Pages_Product_Site.Services;

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
