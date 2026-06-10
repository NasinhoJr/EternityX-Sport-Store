using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EternityX.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProduct_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "САЩ", "Nike" },
                    { 2, "Германия", "Adidas" },
                    { 3, "Германия", "Puma" },
                    { 4, "САЩ", "Under Armour" },
                    { 5, "САЩ", "Jordan" },
                    { 6, "САЩ", "Wilson" },
                    { 7, "САЩ", "Spalding" },
                    { 8, "Австрия", "Head" },
                    { 9, "Япония", "Asics" },
                    { 10, "САЩ", "Reebok" },
                    { 11, "САЩ", "TRX" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Спортни обувки за бягане и ежедневни тренировки.", "Маратонки за бягане" },
                    { 2, "Копонки, футболни тениски и панталони.", "Футболни обувки и екипи" },
                    { 3, "Баскетболни кецове и официални топки за зала и открито.", "Баскетболни обувки и топки" },
                    { 4, "Обувки и оборудване за фитнес и функционални тренировки.", "Фитнес и тренировки" },
                    { 5, "Ракети, топки и оборудване за тенис.", "Тенис" },
                    { 6, "Спортни обувки за деца за различни активности.", "Детски спортни обувки" },
                    { 7, "Раници, шапки, чанти и други спортни аксесоари.", "Аксесоари" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "Description", "Name", "Picture", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 1, "Черни", "Мъжки маратонки за бягане с добра амортизация и комфорт за ежедневни пробези.", "Nike Air Zoom Pegasus 40", "https://s.shopsector.com/uploads/productgalleryfile/images/1200x1200/maratonki-nike-air-zoom-pegasus-40-dx2498-001-1.jpg", 259.99m, 20 },
                    { 2, 9, 1, "Сини", "Стабилни маратонки за дълги дистанции с гел омекотяване и поддръжка.", "Asics Gel-Kayano 30", "https://cdncloudcart.com/30585/products/images/43381/damski-maratonki-za-bagane-asics-gel-kayano-30-1012b357-405-image_65ba4ad9518c8.webp?1706710173", 289.99m, 15 },
                    { 3, 2, 2, "Червено/Черни", "Футболни обувки за твърди терени с отлично сцепление и контрол върху топката.", "Adidas Predator Accuracy FG", "https://u-mercari-images.mercdn.net/photos/m89046758683_1.jpg", 189.99m, 25 },
                    { 4, 3, 2, "Оранжеви", "Леки футболни обувки, подходящи за естествена и изкуствена трева.", "Puma Ultra Match FG/AG", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7LgkrKRN6S334Kts7PjqJHQ7WYpkFJSLt5Q&s", 159.99m, 30 },
                    { 5, 2, 2, "Бяла", "Лека футболна тениска за тренировки с дишаща материя.", "Adidas Tiro 23 Training Jersey", "https://footballkitsdirect.com/cdn/shop/files/08387-150741_-__0006_hr4610_sf_main_278895_747x.jpg?v=1719151054", 49.99m, 40 },
                    { 6, 5, 3, "Черни/Зелени", "Баскетболни кецове с отлична стабилност и сцепление за динамична игра.", "Jordan Luka 2", "https://www.si.com/.image/c_fill,w_720,ar_16:9,f_auto,q_auto,g_auto/MjAzNTgyMTY5Nzc0MzAzMTYz/luka-unc.jpg", 239.99m, 18 },
                    { 7, 1, 3, "Бяло/Златно", "Леки баскетболни обувки с омекотяване и поддръжка за експлозивни движения.", "Nike Giannis Immortality 3", "https://cdn.plutosport.com/a/ProductMedia/Nike/P.NIKE.SHS.8306/Nike-Giannis-Immortatilty-3-Basketbalschoenen-Heren-2403150902.jpg?profile=max_width_mobile", 189.99m, 22 },
                    { 8, 6, 3, "Кафява", "Професионална баскетболна топка за зала с меко усещане и отличен захват.", "Wilson Evolution Indoor Basketball", "https://www.wilson.com/en-us/media/catalog/product/article_images/WTB0516_/WTB0516__054ba85145dc026b3d5e7ce2f6263fdf.png", 99.99m, 25 },
                    { 9, 7, 3, "Кафява", "FIBA-одобрена баскетболна топка за зала с микрофибърно покритие.", "Spalding TF-1000 Legacy", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkACoxbj7NCMSgx0j3aep0nVFrNGZqR95RFA&s", 109.99m, 20 },
                    { 10, 1, 4, "Сиво/Оранжево", "Тренировъчни обувки с твърда подметка, подходящи за кросфит и тежести.", "Nike Metcon 9", "https://img.modivo.cloud/product(1/a/5/e/1a5ef835b03427be6e5835c6a999c8876b933ab9_20_0197598752487.jpg,jpg)/nike-obuvki-za-fitnes-zala-nike-metcon-9-dz2537-siv-0000305201218.jpg", 219.99m, 16 },
                    { 11, 10, 4, "Черни", "Мултифункционални фитнес обувки за силови и кардио тренировки.", "Reebok Nano X3", "https://i1.t4s.cz/products/hp6041/reebok-nano-x3-556292-hp6041.jpg", 199.99m, 14 },
                    { 12, 4, 4, "Черен/Червен", "Регулируем дъмбел с диапазон от приблизително 2 до 24 кг – идеален за домашен фитнес.", "Регулируем дъмбел 2–24 кг", "https://sportensklad.bg/image/cache/catalog/FITNES_UREDI/tejesti-lostove-stoiki/dumbeli/dambel-baufleks-1-1024x1024.jpg", 249.99m, 10 },
                    { 13, 8, 5, "Оранжево/Сиво", "Лека тенис ракета с по-голяма глава за повече контрол и комфорт.", "Head Radical Team 2023", "https://media.strefatenisa.com.pl/public/media/80/4f/cd/1721084782/radicalteaml2023_1.png?ts=1745861148", 179.99m, 12 },
                    { 14, 6, 5, "Жълти", "Комплект от 4 тенис топки, официално одобрени за US Open.", "Wilson US Open Tennis Balls (4-pack)", "https://www.merchantoftennis.com/cdn/shop/products/s28514_1024x.jpg?v=1551718910", 14.99m, 60 },
                    { 15, 1, 6, "Розови", "Детски маратонки за училище и спорт с мека междинна подметка.", "Nike Revolution 6 Kids", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMYbaXkgh6sikkukybZUueqZNedevU-feoqA&s", 89.99m, 25 },
                    { 16, 2, 6, "Зелени", "Детски футболни обувки за изкуствена и естествена трева.", "Adidas X Speedportal.4 Kids", "https://i.sportisimo.com/products/images/1455/1455761/700x700/adidas-x-speedportal-4-tf-j-grn_4.jpg", 79.99m, 22 },
                    { 17, 1, 7, "Черна", "Тренировъчна раница с голямо основно отделение и странични джобове.", "Nike Brasilia Training Backpack", "https://www.bestbuysoccer.com/cdn/shop/products/nike-brasilia-training-backpack-1316480.jpg?v=1752535733", 69.99m, 18 },
                    { 18, 4, 7, "Тъмносин", "Спортен сак с водоустойчива основа и множество отделения.", "Under Armour Undeniable Duffel 4.0", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRo8Eck8X8EPkfVcMQAm4pn4EB19Ki0SYty5g&s", 79.99m, 15 },
                    { 19, 3, 7, "Черна", "Класическа спортна шапка с извита козирка.", "Puma Classic Cap", "https://cdn.deporvillage.com/cdn-cgi/image/h=960,w=768,dpr=1,f=auto,q=75,fit=contain,background=white/product-vertical/pum-025678-01_003.jpg", 29.99m, 35 },
                    { 20, 10, 7, "Лилава", "Удобна постелка за йога и стречинг, с нехлъзгащо покритие.", "Йога постелка с нехлъзгаща повърхност", "https://smfit.bg/cdn/shop/files/iogapostelkapostelkazauprajneniqrozovcvqt.jpg?v=1764233618", 39.99m, 28 },
                    { 21, 1, 1, "Сиви", "Маратонки за бягане с отлично омекотяване и стабилност, подходящи за дълги дистанции.", "Nike React Infinity Run Flyknit", "https://i1.t4s.cz/products/dd3024-005/nike-react-infinity-run-flyknit-3-444299-dd3024-005.jpeg", 219.99m, 21 },
                    { 22, 4, 1, "Черни", "Бягащи обувки с HOVR технология за омекотяване и енергийна възвръщаемост.", "Under Armour HOVR Phantom 2", "https://i.sportisimo.com/products/images/1131/1131897/700x700/under-armour-3023017-101-hovr-phantom-2_2.jpg", 189.99m, 20 },
                    { 23, 1, 1, "Сини", "Маратонки за бягане, предлагащи стабилност и комфорт на всяка крачка.", "Nike Air Zoom Structure 24", "https://i1.t4s.cz/products/da8535-401/nike-air-zoom-structure-24-468859-da8535-401.jpeg", 169.99m, 15 },
                    { 24, 1, 3, "Червено/Златно", "Баскетболни обувки, които осигуряват отлична стабилност и комфорт за големи натоварвания.", "Nike LeBron 20", "https://static.nike.com/a/images/f_auto,cs_srgb/w_1920,c_limit/94d90b38-020e-4a4d-b80a-5cd73fae3be0/official-lebron-20-release-info-date.jpg", 299.99m, 10 },
                    { 25, 2, 3, "Черни/Бели", "Баскетболни обувки с висока поддръжка и отличен комфорт за игра по целия терен.", "Adidas Harden Vol. 7", "https://i1.t4s.cz//products/hq3425-11/adidas-harden-volume-7-576869-hq3425-14-960.webp", 239.99m, 12 },
                    { 26, 10, 4, "Черни/Сини", "Мултифункционални фитнес обувки с отлична поддръжка за тренировки със силови и кардио упражнения.", "Reebok Nano X2", "https://www.sportihobi.bg/sites/default/files/styles/uc_product_full/public/201161790/01161790_xxl.jpg?itok=XImNRZ_s", 189.99m, 17 },
                    { 27, 11, 4, "Черно/Жълто", "Системата за тренировки с тежест на тялото TRX за пълноценна тренировка в дома.", "TRX Home2 Suspension Trainer", "https://www.trxtraining.com/cdn/shop/products/TRXHome2-5-21-21v21312_R2.jpg?v=1760621193", 159.99m, 10 },
                    { 28, 1, 1, "Сини", "Леки маратонки за бягане с отлична амортизация и подкрепа за вашите стъпала.", "Nike Air Zoom Winflo 9", "https://i.sportisimo.com/products/images/1595/1595522/700x700/nike-air-winflo-9_2.jpg", 129.99m, 22 },
                    { 29, 3, 1, "Червени", "Бягащи обувки с амортизация Nitro, осигуряващи комфорт и подкрепа за всяка крачка.", "Puma Velocity Nitro 2", "https://img.eobuwie.cloud/product(6/e/1/2/6e128842bc25e6ed7fc3ea730574712c8c413c13_01_0000300981085_swa_1,jpg)/obuvki-puma-velocity-nitro-2-wns-376262-07-sunset-glow-puma-black.jpg", 139.99m, 20 },
                    { 30, 2, 1, "Черни", "Бягащи обувки с Ultraboost технология за изключителна омекотяване и комфорт.", "Adidas Ultraboost 22", "https://img.eobuwie.cloud/product(d/0/a/1/d0a175a60d7f1fef42fce65c21a6cd591e8d353c_0000209423570_08_pl.jpg,jpg)/obuvki-adidas-ultraboost-22-gx3062-cblack-cblack-ftwwht.jpg", 179.99m, 16 },
                    { 31, 1, 3, "Черни/Червени", "Баскетболни обувки с изключително сцепление и поддръжка, подходящи за бързи движения на терена.", "Nike Kyrie 8", "https://www.sportvision.bg/files/thumbs/files/images/slike_proizvoda/media/CZ0/CZ0204-001/images/thumbs_800/CZ0204-001_800_800px.jpg", 229.99m, 14 },
                    { 32, 2, 3, "Черно/Синьо", "Баскетболни обувки с отлична стабилност и сцепление за бързи и силни движения.", "Adidas Dame 8", "https://i1.t4s.cz//products/gz6475/adidas-dame-8-461657-gz6475-960.webp", 239.99m, 15 },
                    { 33, 4, 3, "Сини/Зелени", "Баскетболни обувки с отлична стабилност и омекотяване, подходящи за нападателни действия.", "Under Armour Curry 10", "https://i1.t4s.cz//products/3025622-300/under-armour-curry-10-spk-blu-520963-3025622-300-960.webp", 249.99m, 13 },
                    { 34, 3, 3, "Черно/Бяло", "Баскетболни обувки с уникален дизайн и сцепление за уверено движение на терена.", "Puma RS-X Basketball", "https://m.media-amazon.com/images/I/41HjLqj+6jL._AC_UY900_.jpg", 169.99m, 10 },
                    { 35, 2, 2, "Червени", "Футболни обувки за бързи движения с контролиран стил.", "Adidas X Speedflow+", "https://gfx.r-gol.com/media/res/products/577/146577/465x605/adidas-x-speedflow-fg_9.png", 199.99m, 23 },
                    { 36, 1, 2, "Черни", "Елитни футболни обувки за всички позиции с отлична издръжливост.", "Nike Tiempo Legend 9", "https://i1.t4s.cz/products/cz8482-007/nike-tiempo-legend-9-elite-fg-451800-cz8482-007.jpg", 249.99m, 28 },
                    { 37, 2, 2, "Черни", "Класически обувки с невероятно усещане за топката.", "Adidas Copa Mundial", "https://i1.t4s.cz/products/015110/adidas-copa-mundial-170333-015112.jpg", 169.99m, 24 },
                    { 38, 3, 2, "Зелени", "Леки футболни обувки с отлична поддръжка за динамична игра.", "Puma Future Z", "https://i1.t4s.cz//products/10174912/puma-future-z-1-1-fg-ag-316369-10174917-960.webp", 179.99m, 23 },
                    { 39, 1, 2, "Червени", "Изключителна скорост и контрол върху топката за атакуващи играчи.", "Nike Phantom GT2", "https://gfx.r-gol.com/media/res/products/951/147951/nike-phantom-gt2-elite-fg_1.jpg", 219.99m, 28 },
                    { 40, 9, 2, "Сини", "Футболни обувки с максимално сцепление и стабилност.", "Asics Gel-Blast", "https://i.sportisimo.com/products/images/418/418037/700x700/asics-gel-blast-7_0.jpg", 159.99m, 30 },
                    { 41, 3, 2, "Бели", "Луксозни обувки за футбол с висока издръжливост.", "Puma King Platinum", "https://i.sportisimo.com/products/images/957/957517/700x700/puma-10560601-king-platinum-fg-ag_0.jpg", 249.99m, 21 },
                    { 42, 4, 2, "Червени", "Футболни обувки с отлична стабилност за защитници.", "Under Armour Spotlight", "https://i.sportisimo.com/products/images/376/376743/700x700/under-armour-1272302-669-ua-spotlight-dl-fg-rtr_2.jpg", 199.99m, 23 },
                    { 43, 2, 2, "Черни", "Обувки за футбол с висока поддръжка и подчертан контрол.", "Adidas Predator Freak", "https://i1.t4s.cz/products/fy6257/adidas-predator-freak-1-fg-362443-fy6260.jpg", 239.99m, 25 },
                    { 44, 3, 2, "Сини", "Модерни футболни обувки за скорост и контрол.", "Puma Future Z 1.1", "https://www.golgeter-shop.com/wp-content/uploads/2021/08/buty-pilkarskie-puma-future-z-1-2-fg-ag-m-106476-01-niebieskie-wielokolorowe-790x790-1.jpeg", 189.99m, 30 },
                    { 45, 10, 4, "Червени", "Фитнес обувки за кросфит с отлична поддръжка.", "Reebok CrossFit Nano 2.0", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhcqGpThEOIfIaYOuIY33LZnRll68gnMRxDg&s", 199.99m, 25 },
                    { 46, 2, 4, "Черни", "Обувки за тежести с отлична стабилност и комфорт.", "Adidas Powerlift 4", "https://varuste.net/tiedostot/1/kuva/tuote/alkuperainen/12928067.jpg", 159.99m, 30 },
                    { 47, 1, 4, "Сини", "Мултифункционални фитнес обувки за всякакви тренировки.", "Nike Metcon 7", "https://www.sportvision.bg/files/images/slike_proizvoda/media/DC9/DC9510-199/images/DC9510-199.jpg", 219.99m, 30 },
                    { 48, 10, 4, "Бели", "Обувки за фитнес и кросфит с отличен комфорт.", "Reebok Nano X1", "https://static.qns.digital/img/p/1/3/5/0/4/9/6/1350496-full_product.jpg", 199.99m, 30 },
                    { 49, 4, 4, "Сиви", "Изключителни фитнес обувки за тежки тренировки.", "Under Armour TriBase Reign 3", "https://i1.t4s.cz/products/3027341-102/under-armour-ua-tribase-reign-6-gry-709748-3027341-103.jpg", 229.99m, 30 },
                    { 50, 5, 4, "Черни", "Силни и комфортни обувки за фитнес и тренировки.", "Jordan Trunner LX", "https://static.ftshp.digital/img/p/1/6/7/7/6/3/4/1677634-full_product.jpg", 189.99m, 28 },
                    { 51, 2, 4, "Сини", "Фитнес обувки за стабилност по време на тренировка.", "Adidas Adipure Trainer", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwNYeDR-rA4lAEEurKNE519bp4kkMxB-LOjg&s", 159.99m, 28 },
                    { 52, 6, 4, "Жълти", "Модерни фитнес обувки за тренировки на различни платформи.", "Wilson Fitness", "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=WMHPSW-1.jpg&nw=455", 179.99m, 30 },
                    { 53, 1, 4, "Лилави", "Идеални за тренировки с висока интензивност.", "Nike Free X Metcon 3", "https://cdna.lystit.com/photos/nike/1db1f33f/nike-Red-Free-Metcon-5-Workout-Shoes.jpeg", 239.99m, 28 },
                    { 54, 10, 4, "Бели", "Обувки за интензивни тренировки с отличен комфорт и издръжливост.", "Reebok Speed TR 2.0", "https://i1.t4s.cz/products/dv9563/reebok-speed-tr-flexweave-232624-dv9563.jpg", 169.99m, 28 }
                });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                columns: new[] { "Id", "ProductId", "Quantity", "Size" },
                values: new object[,]
                {
                    { 1, 1, 4, "41" },
                    { 2, 1, 5, "42" },
                    { 3, 1, 5, "43" },
                    { 4, 1, 4, "44" },
                    { 5, 1, 2, "45" },
                    { 6, 2, 3, "41" },
                    { 7, 2, 4, "42" },
                    { 8, 2, 4, "43" },
                    { 9, 2, 2, "44" },
                    { 10, 2, 2, "45" },
                    { 11, 3, 6, "41" },
                    { 12, 3, 6, "42" },
                    { 13, 3, 5, "43" },
                    { 14, 3, 4, "44" },
                    { 15, 3, 4, "45" },
                    { 16, 4, 8, "41" },
                    { 17, 4, 7, "42" },
                    { 18, 4, 6, "43" },
                    { 19, 4, 5, "44" },
                    { 20, 4, 4, "45" },
                    { 21, 5, 10, "S" },
                    { 22, 5, 12, "M" },
                    { 23, 5, 10, "L" },
                    { 24, 5, 8, "XL" },
                    { 25, 6, 4, "41" },
                    { 26, 6, 4, "42" },
                    { 27, 6, 4, "43" },
                    { 28, 6, 3, "44" },
                    { 29, 6, 3, "45" },
                    { 30, 7, 4, "41" },
                    { 31, 7, 5, "42" },
                    { 32, 7, 5, "43" },
                    { 33, 7, 4, "44" },
                    { 34, 7, 4, "45" },
                    { 35, 8, 25, "7" },
                    { 36, 9, 20, "7" },
                    { 37, 10, 3, "41" },
                    { 38, 10, 4, "42" },
                    { 39, 10, 4, "43" },
                    { 40, 10, 3, "44" },
                    { 41, 10, 2, "45" },
                    { 42, 11, 3, "41" },
                    { 43, 11, 4, "42" },
                    { 44, 11, 4, "43" },
                    { 45, 11, 2, "44" },
                    { 46, 11, 1, "45" },
                    { 47, 12, 10, "2–24 кг" },
                    { 48, 13, 12, "102 in²" },
                    { 49, 14, 60, "Стандартни" },
                    { 50, 15, 6, "33" },
                    { 51, 15, 6, "34" },
                    { 52, 15, 7, "35" },
                    { 53, 15, 6, "36" },
                    { 54, 16, 5, "33" },
                    { 55, 16, 5, "34" },
                    { 56, 16, 6, "35" },
                    { 57, 16, 6, "36" },
                    { 58, 18, 7, "Малка" },
                    { 59, 18, 8, "Голяма" },
                    { 60, 17, 18, "Един размер" },
                    { 61, 19, 35, "Един размер" },
                    { 62, 20, 28, "Стандартна" },
                    { 63, 21, 4, "41" },
                    { 64, 21, 6, "42" },
                    { 65, 21, 5, "43" },
                    { 66, 21, 3, "44" },
                    { 67, 21, 3, "45" },
                    { 68, 22, 5, "41" },
                    { 69, 22, 5, "42" },
                    { 70, 22, 4, "43" },
                    { 71, 22, 3, "44" },
                    { 72, 22, 3, "45" },
                    { 73, 23, 4, "41" },
                    { 74, 23, 4, "42" },
                    { 75, 23, 4, "43" },
                    { 76, 23, 2, "44" },
                    { 77, 23, 1, "45" },
                    { 78, 24, 2, "41" },
                    { 79, 24, 3, "42" },
                    { 80, 24, 3, "43" },
                    { 81, 24, 2, "44" },
                    { 82, 25, 3, "41" },
                    { 83, 25, 3, "42" },
                    { 84, 25, 3, "43" },
                    { 85, 25, 3, "44" },
                    { 86, 26, 4, "41" },
                    { 87, 26, 5, "42" },
                    { 88, 26, 5, "43" },
                    { 89, 26, 3, "44" },
                    { 90, 27, 10, "Един размер" },
                    { 91, 28, 4, "41" },
                    { 92, 28, 5, "42" },
                    { 93, 28, 6, "43" },
                    { 94, 28, 4, "44" },
                    { 95, 28, 3, "45" },
                    { 96, 29, 4, "41" },
                    { 97, 29, 5, "42" },
                    { 98, 29, 5, "43" },
                    { 99, 29, 4, "44" },
                    { 100, 29, 2, "45" },
                    { 101, 30, 4, "41" },
                    { 102, 30, 4, "42" },
                    { 103, 30, 4, "43" },
                    { 104, 30, 2, "44" },
                    { 105, 30, 2, "45" },
                    { 106, 31, 3, "41" },
                    { 107, 31, 4, "42" },
                    { 108, 31, 4, "43" },
                    { 109, 31, 3, "44" },
                    { 110, 32, 4, "41" },
                    { 111, 32, 5, "42" },
                    { 112, 32, 4, "43" },
                    { 113, 32, 2, "44" },
                    { 114, 33, 3, "41" },
                    { 115, 33, 4, "42" },
                    { 116, 33, 4, "43" },
                    { 117, 33, 2, "44" },
                    { 118, 34, 3, "41" },
                    { 119, 34, 3, "42" },
                    { 120, 34, 2, "43" },
                    { 121, 34, 2, "44" },
                    { 122, 35, 5, "41" },
                    { 123, 35, 6, "42" },
                    { 124, 35, 5, "43" },
                    { 125, 35, 4, "44" },
                    { 126, 35, 3, "45" },
                    { 127, 36, 6, "41" },
                    { 128, 36, 7, "42" },
                    { 129, 36, 6, "43" },
                    { 130, 36, 5, "44" },
                    { 131, 36, 4, "45" },
                    { 132, 37, 5, "41" },
                    { 133, 37, 6, "42" },
                    { 134, 37, 5, "43" },
                    { 135, 37, 4, "44" },
                    { 136, 37, 4, "45" },
                    { 137, 38, 5, "41" },
                    { 138, 38, 6, "42" },
                    { 139, 38, 5, "43" },
                    { 140, 38, 4, "44" },
                    { 141, 38, 3, "45" },
                    { 142, 39, 6, "41" },
                    { 143, 39, 7, "42" },
                    { 144, 39, 6, "43" },
                    { 145, 39, 5, "44" },
                    { 146, 39, 4, "45" },
                    { 147, 40, 7, "41" },
                    { 148, 40, 8, "42" },
                    { 149, 40, 6, "43" },
                    { 150, 40, 5, "44" },
                    { 151, 40, 4, "45" },
                    { 152, 41, 6, "41" },
                    { 153, 41, 7, "42" },
                    { 154, 41, 5, "43" },
                    { 155, 41, 4, "44" },
                    { 156, 41, 3, "45" },
                    { 157, 42, 5, "41" },
                    { 158, 42, 6, "42" },
                    { 159, 42, 5, "43" },
                    { 160, 42, 4, "44" },
                    { 161, 42, 3, "45" },
                    { 162, 43, 6, "41" },
                    { 163, 43, 7, "42" },
                    { 164, 43, 5, "43" },
                    { 165, 43, 4, "44" },
                    { 166, 43, 3, "45" },
                    { 167, 44, 7, "41" },
                    { 168, 44, 8, "42" },
                    { 169, 44, 6, "43" },
                    { 170, 44, 5, "44" },
                    { 171, 44, 4, "45" },
                    { 172, 45, 6, "41" },
                    { 173, 45, 7, "42" },
                    { 174, 45, 6, "43" },
                    { 175, 45, 5, "44" },
                    { 176, 45, 4, "45" },
                    { 177, 46, 7, "41" },
                    { 178, 46, 8, "42" },
                    { 179, 46, 6, "43" },
                    { 180, 46, 5, "44" },
                    { 181, 46, 4, "45" },
                    { 182, 47, 7, "41" },
                    { 183, 47, 8, "42" },
                    { 184, 47, 6, "43" },
                    { 185, 47, 5, "44" },
                    { 186, 47, 4, "45" },
                    { 187, 48, 7, "41" },
                    { 188, 48, 8, "42" },
                    { 189, 48, 6, "43" },
                    { 190, 48, 5, "44" },
                    { 191, 48, 4, "45" },
                    { 192, 49, 7, "41" },
                    { 193, 49, 8, "42" },
                    { 194, 49, 6, "43" },
                    { 195, 49, 5, "44" },
                    { 196, 49, 4, "45" },
                    { 197, 50, 6, "41" },
                    { 198, 50, 7, "42" },
                    { 199, 50, 6, "43" },
                    { 200, 50, 5, "44" },
                    { 201, 50, 4, "45" },
                    { 202, 51, 6, "41" },
                    { 203, 51, 7, "42" },
                    { 204, 51, 6, "43" },
                    { 205, 51, 5, "44" },
                    { 206, 51, 4, "45" },
                    { 207, 52, 7, "41" },
                    { 208, 52, 8, "42" },
                    { 209, 52, 6, "43" },
                    { 210, 52, 5, "44" },
                    { 211, 52, 4, "45" },
                    { 212, 53, 6, "41" },
                    { 213, 53, 7, "42" },
                    { 214, 53, 6, "43" },
                    { 215, 53, 5, "44" },
                    { 216, 53, 4, "45" },
                    { 217, 54, 6, "41" },
                    { 218, 54, 7, "42" },
                    { 219, 54, 6, "43" },
                    { 220, 54, 5, "44" },
                    { 221, 54, 4, "45" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_UserId",
                table: "ContactMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProduct_ApplicationUserId",
                table: "FavoriteProduct",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProduct_ProductId",
                table: "FavoriteProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ApplicationUserId",
                table: "ShoppingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "FavoriteProduct");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
