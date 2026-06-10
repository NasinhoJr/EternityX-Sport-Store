using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EternityX.Models;

namespace EternityX.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        
        public DbSet<ProductSize> ProductSizes { get; set; }

       
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Brand>().HasData(
              new Brand { Id = 1, Name = "Nike", Country = "САЩ" },
              new Brand { Id = 2, Name = "Adidas", Country = "Германия" },
              new Brand { Id = 3, Name = "Puma", Country = "Германия" },
              new Brand { Id = 4, Name = "Under Armour", Country = "САЩ" },
              new Brand { Id = 5, Name = "Jordan", Country = "САЩ" },
              new Brand { Id = 6, Name = "Wilson", Country = "САЩ" },
              new Brand { Id = 7, Name = "Spalding", Country = "САЩ" },
              new Brand { Id = 8, Name = "Head", Country = "Австрия" },
              new Brand { Id = 9, Name = "Asics", Country = "Япония" },
              new Brand { Id = 10, Name = "Reebok", Country = "САЩ" },
              new Brand { Id=11,Name="TRX",Country="САЩ"}
          );

            builder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Name = "Маратонки за бягане",
                   Description = "Спортни обувки за бягане и ежедневни тренировки."
               },
               new Category
               {
                   Id = 2,
                   Name = "Футболни обувки и екипи",
                   Description = "Копонки, футболни тениски и панталони."
               },
               new Category
               {
                   Id = 3,
                   Name = "Баскетболни обувки и топки",
                   Description = "Баскетболни кецове и официални топки за зала и открито."
               },
               new Category
               {
                   Id = 4,
                   Name = "Фитнес и тренировки",
                   Description = "Обувки и оборудване за фитнес и функционални тренировки."
               },
               new Category
               {
                   Id = 5,
                   Name = "Тенис",
                   Description = "Ракети, топки и оборудване за тенис."
               },
               new Category
               {
                   Id = 6,
                   Name = "Детски спортни обувки",
                   Description = "Спортни обувки за деца за различни активности."
               },
               new Category
               {
                   Id = 7,
                   Name = "Аксесоари",
                   Description = "Раници, шапки, чанти и други спортни аксесоари."
               }
           );

            builder.Entity<Product>().HasData(
    // БЯГАНЕ
    new Product
    {
        Id = 1,
        Name = "Nike Air Zoom Pegasus 40",
        Description = "Мъжки маратонки за бягане с добра амортизация и комфорт за ежедневни пробези.",
        Price = 259.99m,
        Stock = 20,
        Color = "Черни",
        BrandId = 1,
        CategoryId = 1,
        Picture = "https://s.shopsector.com/uploads/productgalleryfile/images/1200x1200/maratonki-nike-air-zoom-pegasus-40-dx2498-001-1.jpg"
    },
    new Product
    {
        Id = 2,
        Name = "Asics Gel-Kayano 30",
        Description = "Стабилни маратонки за дълги дистанции с гел омекотяване и поддръжка.",
        Price = 289.99m,
        Stock = 15,
        Color = "Сини",
        BrandId = 9,
        CategoryId = 1,
        Picture = "https://cdncloudcart.com/30585/products/images/43381/damski-maratonki-za-bagane-asics-gel-kayano-30-1012b357-405-image_65ba4ad9518c8.webp?1706710173"
    },

    // ФУТБОЛ
    new Product
    {
        Id = 3,
        Name = "Adidas Predator Accuracy FG",
        Description = "Футболни обувки за твърди терени с отлично сцепление и контрол върху топката.",
        Price = 189.99m,
        Stock = 25,
        Color = "Червено/Черни",
        BrandId = 2,
        CategoryId = 2,
        Picture = "https://u-mercari-images.mercdn.net/photos/m89046758683_1.jpg"
    },
    new Product
    {
        Id = 4,
        Name = "Puma Ultra Match FG/AG",
        Description = "Леки футболни обувки, подходящи за естествена и изкуствена трева.",
        Price = 159.99m,
        Stock = 30,
        Color = "Оранжеви",
        BrandId = 3,
        CategoryId = 2,
        Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7LgkrKRN6S334Kts7PjqJHQ7WYpkFJSLt5Q&s"
    },
    new Product
    {
        Id = 5,
        Name = "Adidas Tiro 23 Training Jersey",
        Description = "Лека футболна тениска за тренировки с дишаща материя.",
        Price = 49.99m,
        Stock = 40,
        Color = "Бяла",
        BrandId = 2,
        CategoryId = 2,
        Picture = "https://footballkitsdirect.com/cdn/shop/files/08387-150741_-__0006_hr4610_sf_main_278895_747x.jpg?v=1719151054"
    },

    // БАСКЕТБОЛ – ОБУВКИ
    new Product
    {
        Id = 6,
        Name = "Jordan Luka 2",
        Description = "Баскетболни кецове с отлична стабилност и сцепление за динамична игра.",
        Price = 239.99m,
        Stock = 18,
        Color = "Черни/Зелени",
        BrandId = 5,
        CategoryId = 3,
        Picture = "https://www.si.com/.image/c_fill,w_720,ar_16:9,f_auto,q_auto,g_auto/MjAzNTgyMTY5Nzc0MzAzMTYz/luka-unc.jpg"
    },
    new Product
    {
        Id = 7,
        Name = "Nike Giannis Immortality 3",
        Description = "Леки баскетболни обувки с омекотяване и поддръжка за експлозивни движения.",
        Price = 189.99m,
        Stock = 22,
        Color = "Бяло/Златно",
        BrandId = 1,
        CategoryId = 3,
        Picture = "https://cdn.plutosport.com/a/ProductMedia/Nike/P.NIKE.SHS.8306/Nike-Giannis-Immortatilty-3-Basketbalschoenen-Heren-2403150902.jpg?profile=max_width_mobile"
    },

    // БАСКЕТБОЛ – ТОПКИ
    new Product
    {
        Id = 8,
        Name = "Wilson Evolution Indoor Basketball",
        Description = "Професионална баскетболна топка за зала с меко усещане и отличен захват.",
        Price = 99.99m,
        Stock = 25,
        Color = "Кафява",
        BrandId = 6,
        CategoryId = 3,
        Picture = "https://www.wilson.com/en-us/media/catalog/product/article_images/WTB0516_/WTB0516__054ba85145dc026b3d5e7ce2f6263fdf.png"
    },
    new Product
    {
        Id = 9,
        Name = "Spalding TF-1000 Legacy",
        Description = "FIBA-одобрена баскетболна топка за зала с микрофибърно покритие.",
        Price = 109.99m,
        Stock = 20,
        Color = "Кафява",
        BrandId = 7,
        CategoryId = 3,
        Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkACoxbj7NCMSgx0j3aep0nVFrNGZqR95RFA&s"
    },

    // ФИТНЕС – ОБУВКИ + ОБОРУДВАНЕ
    new Product
    {
        Id = 10,
        Name = "Nike Metcon 9",
        Description = "Тренировъчни обувки с твърда подметка, подходящи за кросфит и тежести.",
        Price = 219.99m,
        Stock = 16,
        Color = "Сиво/Оранжево",
        BrandId = 1,
        CategoryId = 4,
        Picture = "https://img.modivo.cloud/product(1/a/5/e/1a5ef835b03427be6e5835c6a999c8876b933ab9_20_0197598752487.jpg,jpg)/nike-obuvki-za-fitnes-zala-nike-metcon-9-dz2537-siv-0000305201218.jpg"
    },
    new Product
    {
        Id = 11,
        Name = "Reebok Nano X3",
        Description = "Мултифункционални фитнес обувки за силови и кардио тренировки.",
        Price = 199.99m,
        Stock = 14,
        Color = "Черни",
        BrandId = 10,
        CategoryId = 4,
        Picture = "https://i1.t4s.cz/products/hp6041/reebok-nano-x3-556292-hp6041.jpg"
    },
    new Product
    {
        Id = 12,
        Name = "Регулируем дъмбел 2–24 кг",
        Description = "Регулируем дъмбел с диапазон от приблизително 2 до 24 кг – идеален за домашен фитнес.",
        Price = 249.99m,
        Stock = 10,
        Color = "Черен/Червен",
        BrandId = 4,
        CategoryId = 4,
        Picture = "https://sportensklad.bg/image/cache/catalog/FITNES_UREDI/tejesti-lostove-stoiki/dumbeli/dambel-baufleks-1-1024x1024.jpg"
    },

    // ТЕНИС
    new Product
    {
        Id = 13,
        Name = "Head Radical Team 2023",
        Description = "Лека тенис ракета с по-голяма глава за повече контрол и комфорт.",
        Price = 179.99m,
        Stock = 12,
        Color = "Оранжево/Сиво",
        BrandId = 8,
        CategoryId = 5,
        Picture = "https://media.strefatenisa.com.pl/public/media/80/4f/cd/1721084782/radicalteaml2023_1.png?ts=1745861148"
    },
    new Product
    {
        Id = 14,
        Name = "Wilson US Open Tennis Balls (4-pack)",
        Description = "Комплект от 4 тенис топки, официално одобрени за US Open.",
        Price = 14.99m,
        Stock = 60,
        Color = "Жълти",
        BrandId = 6,
        CategoryId = 5,
        Picture = "https://www.merchantoftennis.com/cdn/shop/products/s28514_1024x.jpg?v=1551718910"
    },

    // ДЕТСКИ
    new Product
    {
        Id = 15,
        Name = "Nike Revolution 6 Kids",
        Description = "Детски маратонки за училище и спорт с мека междинна подметка.",
        Price = 89.99m,
        Stock = 25,
        Color = "Розови",
        BrandId = 1,
        CategoryId = 6,
        Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMYbaXkgh6sikkukybZUueqZNedevU-feoqA&s"
    },
    new Product
    {
        Id = 16,
        Name = "Adidas X Speedportal.4 Kids",
        Description = "Детски футболни обувки за изкуствена и естествена трева.",
        Price = 79.99m,
        Stock = 22,
        Color = "Зелени",
        BrandId = 2,
        CategoryId = 6,
        Picture = "https://i.sportisimo.com/products/images/1455/1455761/700x700/adidas-x-speedportal-4-tf-j-grn_4.jpg"
    },

    // АКСЕСОАРИ
    new Product
    {
        Id = 17,
        Name = "Nike Brasilia Training Backpack",
        Description = "Тренировъчна раница с голямо основно отделение и странични джобове.",
        Price = 69.99m,
        Stock = 18,
        Color = "Черна",
        BrandId = 1,
        CategoryId = 7,
        Picture = "https://www.bestbuysoccer.com/cdn/shop/products/nike-brasilia-training-backpack-1316480.jpg?v=1752535733"
    },
    new Product
    {
        Id = 18,
        Name = "Under Armour Undeniable Duffel 4.0",
        Description = "Спортен сак с водоустойчива основа и множество отделения.",
        Price = 79.99m,
        Stock = 15,
        Color = "Тъмносин",
        BrandId = 4,
        CategoryId = 7,
        Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRo8Eck8X8EPkfVcMQAm4pn4EB19Ki0SYty5g&s"
    },
    new Product
    {
        Id = 19,
        Name = "Puma Classic Cap",
        Description = "Класическа спортна шапка с извита козирка.",
        Price = 29.99m,
        Stock = 35,
        Color = "Черна",
        BrandId = 3,
        CategoryId = 7,
        Picture = "https://cdn.deporvillage.com/cdn-cgi/image/h=960,w=768,dpr=1,f=auto,q=75,fit=contain,background=white/product-vertical/pum-025678-01_003.jpg"
    },
    new Product
    {
        Id = 20,
        Name = "Йога постелка с нехлъзгаща повърхност",
        Description = "Удобна постелка за йога и стречинг, с нехлъзгащо покритие.",
        Price = 39.99m,
        Stock = 28,
        Color = "Лилава",
        BrandId = 10,
        CategoryId = 7,
        Picture = "https://smfit.bg/cdn/shop/files/iogapostelkapostelkazauprajneniqrozovcvqt.jpg?v=1764233618"
    },
     new Product
     {
         Id = 21,
         Name = "Nike React Infinity Run Flyknit",
         Description = "Маратонки за бягане с отлично омекотяване и стабилност, подходящи за дълги дистанции.",
         Price = 219.99m,
         Stock = 21,
         Color = "Сиви",
         BrandId = 1,  // Nike
         CategoryId = 1, // Маратонки за бягане
         Picture = "https://i1.t4s.cz/products/dd3024-005/nike-react-infinity-run-flyknit-3-444299-dd3024-005.jpeg"
     },
    new Product
    {
        Id = 22,
        Name = "Under Armour HOVR Phantom 2",
        Description = "Бягащи обувки с HOVR технология за омекотяване и енергийна възвръщаемост.",
        Price = 189.99m,
        Stock = 20,
        Color = "Черни",
        BrandId = 4,  // Under Armour
        CategoryId = 1, // Маратонки за бягане
        Picture = "https://i.sportisimo.com/products/images/1131/1131897/700x700/under-armour-3023017-101-hovr-phantom-2_2.jpg"
    },
    new Product
    {
        Id = 23,
        Name = "Nike Air Zoom Structure 24",
        Description = "Маратонки за бягане, предлагащи стабилност и комфорт на всяка крачка.",
        Price = 169.99m,
        Stock = 15,
        Color = "Сини",
        BrandId = 1,  // Nike
        CategoryId = 1, // Маратонки за бягане
        Picture = "https://i1.t4s.cz/products/da8535-401/nike-air-zoom-structure-24-468859-da8535-401.jpeg"
    },
    new Product
    {
        Id = 24,
        Name = "Nike LeBron 20",
        Description = "Баскетболни обувки, които осигуряват отлична стабилност и комфорт за големи натоварвания.",
        Price = 299.99m,
        Stock = 10,
        Color = "Червено/Златно",
        BrandId = 1,  // Nike
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://static.nike.com/a/images/f_auto,cs_srgb/w_1920,c_limit/94d90b38-020e-4a4d-b80a-5cd73fae3be0/official-lebron-20-release-info-date.jpg"
    },
    new Product
    {
        Id = 25,
        Name = "Adidas Harden Vol. 7",
        Description = "Баскетболни обувки с висока поддръжка и отличен комфорт за игра по целия терен.",
        Price = 239.99m,
        Stock = 12,
        Color = "Черни/Бели",
        BrandId = 2,  // Adidas
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://i1.t4s.cz//products/hq3425-11/adidas-harden-volume-7-576869-hq3425-14-960.webp"
    },
    new Product
    {
        Id = 26,
        Name = "Reebok Nano X2",
        Description = "Мултифункционални фитнес обувки с отлична поддръжка за тренировки със силови и кардио упражнения.",
        Price = 189.99m,
        Stock = 17,
        Color = "Черни/Сини",
        BrandId = 10,  // Reebok
        CategoryId = 4, // Фитнес и тренировки
        Picture = "https://www.sportihobi.bg/sites/default/files/styles/uc_product_full/public/201161790/01161790_xxl.jpg?itok=XImNRZ_s"
    },
    new Product
    {
        Id = 27,
        Name = "TRX Home2 Suspension Trainer",
        Description = "Системата за тренировки с тежест на тялото TRX за пълноценна тренировка в дома.",
        Price = 159.99m,
        Stock = 10,
        Color = "Черно/Жълто",
        BrandId = 11,  // TRX
        CategoryId = 4, // Фитнес и тренировки
        Picture = "https://www.trxtraining.com/cdn/shop/products/TRXHome2-5-21-21v21312_R2.jpg?v=1760621193"
    },
    new Product
    {
        Id = 28,
        Name = "Nike Air Zoom Winflo 9",
        Description = "Леки маратонки за бягане с отлична амортизация и подкрепа за вашите стъпала.",
        Price = 129.99m,
        Stock = 22,
        Color = "Сини",
        BrandId = 1,  // Nike
        CategoryId = 1, // Маратонки за бягане
        Picture = "https://i.sportisimo.com/products/images/1595/1595522/700x700/nike-air-winflo-9_2.jpg"
    },
    new Product
    {
        Id = 29,
        Name = "Puma Velocity Nitro 2",
        Description = "Бягащи обувки с амортизация Nitro, осигуряващи комфорт и подкрепа за всяка крачка.",
        Price = 139.99m,
        Stock = 20,
        Color = "Червени",
        BrandId = 3,  // Puma
        CategoryId = 1, // Маратонки за бягане
        Picture = "https://img.eobuwie.cloud/product(6/e/1/2/6e128842bc25e6ed7fc3ea730574712c8c413c13_01_0000300981085_swa_1,jpg)/obuvki-puma-velocity-nitro-2-wns-376262-07-sunset-glow-puma-black.jpg"
    },
    new Product
    {
        Id = 30,
        Name = "Adidas Ultraboost 22",
        Description = "Бягащи обувки с Ultraboost технология за изключителна омекотяване и комфорт.",
        Price = 179.99m,
        Stock = 16,
        Color = "Черни",
        BrandId = 2,  // Adidas
        CategoryId = 1, // Маратонки за бягане
        Picture = "https://img.eobuwie.cloud/product(d/0/a/1/d0a175a60d7f1fef42fce65c21a6cd591e8d353c_0000209423570_08_pl.jpg,jpg)/obuvki-adidas-ultraboost-22-gx3062-cblack-cblack-ftwwht.jpg"
    },
    new Product
    {
        Id = 31,
        Name = "Nike Kyrie 8",
        Description = "Баскетболни обувки с изключително сцепление и поддръжка, подходящи за бързи движения на терена.",
        Price = 229.99m,
        Stock = 14,
        Color = "Черни/Червени",
        BrandId = 1,  // Nike
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://www.sportvision.bg/files/thumbs/files/images/slike_proizvoda/media/CZ0/CZ0204-001/images/thumbs_800/CZ0204-001_800_800px.jpg"
    },
    new Product
    {
        Id = 32,
        Name = "Adidas Dame 8",
        Description = "Баскетболни обувки с отлична стабилност и сцепление за бързи и силни движения.",
        Price = 239.99m,
        Stock = 15,
        Color = "Черно/Синьо",
        BrandId = 2,  // Adidas
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://i1.t4s.cz//products/gz6475/adidas-dame-8-461657-gz6475-960.webp"
    },
    new Product
    {
        Id = 33,
        Name = "Under Armour Curry 10",
        Description = "Баскетболни обувки с отлична стабилност и омекотяване, подходящи за нападателни действия.",
        Price = 249.99m,
        Stock = 13,
        Color = "Сини/Зелени",
        BrandId = 4,  // Under Armour
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://i1.t4s.cz//products/3025622-300/under-armour-curry-10-spk-blu-520963-3025622-300-960.webp"
    },
    new Product
    {
        Id = 34,
        Name = "Puma RS-X Basketball",
        Description = "Баскетболни обувки с уникален дизайн и сцепление за уверено движение на терена.",
        Price = 169.99m,
        Stock = 10,
        Color = "Черно/Бяло",
        BrandId = 3,  // Puma
        CategoryId = 3, // Баскетболни обувки и топки
        Picture = "https://m.media-amazon.com/images/I/41HjLqj+6jL._AC_UY900_.jpg"
    },
      new Product { Id = 35, BrandId = 2, CategoryId = 2, Name = "Adidas X Speedflow+", Description = "Футболни обувки за бързи движения с контролиран стил.", Price = 199.99m, Stock = 23, Color = "Червени", Picture = "https://gfx.r-gol.com/media/res/products/577/146577/465x605/adidas-x-speedflow-fg_9.png" },
    new Product { Id = 36, BrandId = 1, CategoryId = 2, Name = "Nike Tiempo Legend 9", Description = "Елитни футболни обувки за всички позиции с отлична издръжливост.", Price = 249.99m, Stock = 28, Color = "Черни", Picture = "https://i1.t4s.cz/products/cz8482-007/nike-tiempo-legend-9-elite-fg-451800-cz8482-007.jpg" },
    new Product { Id = 37, BrandId = 2, CategoryId = 2, Name = "Adidas Copa Mundial", Description = "Класически обувки с невероятно усещане за топката.", Price = 169.99m, Stock = 24, Color = "Черни", Picture = "https://i1.t4s.cz/products/015110/adidas-copa-mundial-170333-015112.jpg" },
    new Product { Id = 38, BrandId = 3, CategoryId = 2, Name = "Puma Future Z", Description = "Леки футболни обувки с отлична поддръжка за динамична игра.", Price = 179.99m, Stock = 23, Color = "Зелени", Picture = "https://i1.t4s.cz//products/10174912/puma-future-z-1-1-fg-ag-316369-10174917-960.webp" },
    new Product { Id = 39, BrandId = 1, CategoryId = 2, Name = "Nike Phantom GT2", Description = "Изключителна скорост и контрол върху топката за атакуващи играчи.", Price = 219.99m, Stock = 28, Color = "Червени", Picture = "https://gfx.r-gol.com/media/res/products/951/147951/nike-phantom-gt2-elite-fg_1.jpg" },
    new Product { Id = 40, BrandId = 9, CategoryId = 2, Name = "Asics Gel-Blast", Description = "Футболни обувки с максимално сцепление и стабилност.", Price = 159.99m, Stock = 30, Color = "Сини", Picture = "https://i.sportisimo.com/products/images/418/418037/700x700/asics-gel-blast-7_0.jpg" },
    new Product { Id = 41, BrandId = 3, CategoryId = 2, Name = "Puma King Platinum", Description = "Луксозни обувки за футбол с висока издръжливост.", Price = 249.99m, Stock = 21, Color = "Бели", Picture = "https://i.sportisimo.com/products/images/957/957517/700x700/puma-10560601-king-platinum-fg-ag_0.jpg" },
    new Product { Id = 42, BrandId = 4, CategoryId = 2, Name = "Under Armour Spotlight", Description = "Футболни обувки с отлична стабилност за защитници.", Price = 199.99m, Stock = 23, Color = "Червени", Picture = "https://i.sportisimo.com/products/images/376/376743/700x700/under-armour-1272302-669-ua-spotlight-dl-fg-rtr_2.jpg" },
    new Product { Id = 43, BrandId = 2, CategoryId = 2, Name = "Adidas Predator Freak", Description = "Обувки за футбол с висока поддръжка и подчертан контрол.", Price = 239.99m, Stock = 25, Color = "Черни", Picture = "https://i1.t4s.cz/products/fy6257/adidas-predator-freak-1-fg-362443-fy6260.jpg" },
    new Product { Id = 44, BrandId = 3, CategoryId = 2, Name = "Puma Future Z 1.1", Description = "Модерни футболни обувки за скорост и контрол.", Price = 189.99m, Stock = 30, Color = "Сини", Picture = "https://www.golgeter-shop.com/wp-content/uploads/2021/08/buty-pilkarskie-puma-future-z-1-2-fg-ag-m-106476-01-niebieskie-wielokolorowe-790x790-1.jpeg" },
     new Product { Id = 45, BrandId = 10, CategoryId = 4, Name = "Reebok CrossFit Nano 2.0", Description = "Фитнес обувки за кросфит с отлична поддръжка.", Price = 199.99m, Stock = 25, Color = "Червени", Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhcqGpThEOIfIaYOuIY33LZnRll68gnMRxDg&s" },
    new Product { Id = 46, BrandId = 2, CategoryId = 4, Name = "Adidas Powerlift 4", Description = "Обувки за тежести с отлична стабилност и комфорт.", Price = 159.99m, Stock = 30, Color = "Черни", Picture = "https://varuste.net/tiedostot/1/kuva/tuote/alkuperainen/12928067.jpg" },
    new Product { Id = 47, BrandId = 1, CategoryId = 4, Name = "Nike Metcon 7", Description = "Мултифункционални фитнес обувки за всякакви тренировки.", Price = 219.99m, Stock = 30, Color = "Сини", Picture = "https://www.sportvision.bg/files/images/slike_proizvoda/media/DC9/DC9510-199/images/DC9510-199.jpg" },
    new Product { Id = 48, BrandId = 10, CategoryId = 4, Name = "Reebok Nano X1", Description = "Обувки за фитнес и кросфит с отличен комфорт.", Price = 199.99m, Stock = 30, Color = "Бели", Picture = "https://static.qns.digital/img/p/1/3/5/0/4/9/6/1350496-full_product.jpg" },
    new Product { Id = 49, BrandId = 4, CategoryId = 4, Name = "Under Armour TriBase Reign 3", Description = "Изключителни фитнес обувки за тежки тренировки.", Price = 229.99m, Stock = 30, Color = "Сиви", Picture = "https://i1.t4s.cz/products/3027341-102/under-armour-ua-tribase-reign-6-gry-709748-3027341-103.jpg" },
    new Product { Id = 50, BrandId = 5, CategoryId = 4, Name = "Jordan Trunner LX", Description = "Силни и комфортни обувки за фитнес и тренировки.", Price = 189.99m, Stock = 28, Color = "Черни", Picture = "https://static.ftshp.digital/img/p/1/6/7/7/6/3/4/1677634-full_product.jpg" },
    new Product { Id = 51, BrandId = 2, CategoryId = 4, Name = "Adidas Adipure Trainer", Description = "Фитнес обувки за стабилност по време на тренировка.", Price = 159.99m, Stock = 28, Color = "Сини", Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwNYeDR-rA4lAEEurKNE519bp4kkMxB-LOjg&s" },
    new Product { Id = 52, BrandId = 6, CategoryId = 4, Name = "Wilson Fitness", Description = "Модерни фитнес обувки за тренировки на различни платформи.", Price = 179.99m, Stock = 30, Color = "Жълти", Picture = "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=WMHPSW-1.jpg&nw=455" },
    new Product { Id = 53, BrandId = 1, CategoryId = 4, Name = "Nike Free X Metcon 3", Description = "Идеални за тренировки с висока интензивност.", Price = 239.99m, Stock = 28, Color = "Лилави", Picture = "https://cdna.lystit.com/photos/nike/1db1f33f/nike-Red-Free-Metcon-5-Workout-Shoes.jpeg" },
    new Product { Id = 54, BrandId = 10, CategoryId = 4, Name = "Reebok Speed TR 2.0", Description = "Обувки за интензивни тренировки с отличен комфорт и издръжливост.", Price = 169.99m, Stock = 28, Color = "Бели", Picture = "https://i1.t4s.cz/products/dv9563/reebok-speed-tr-flexweave-232624-dv9563.jpg" }
);
            builder.Entity<ProductSize>().HasData(
    new ProductSize { Id = 1, ProductId = 1, Size = "41", Quantity = 4 },
    new ProductSize { Id = 2, ProductId = 1, Size = "42", Quantity = 5 },
    new ProductSize { Id = 3, ProductId = 1, Size = "43", Quantity = 5 },
    new ProductSize { Id = 4, ProductId = 1, Size = "44", Quantity = 4 },
    new ProductSize { Id = 5, ProductId = 1, Size = "45", Quantity = 2 },

    new ProductSize { Id = 6, ProductId = 2, Size = "41", Quantity = 3 },
    new ProductSize { Id = 7, ProductId = 2, Size = "42", Quantity = 4 },
    new ProductSize { Id = 8, ProductId = 2, Size = "43", Quantity = 4 },
    new ProductSize { Id = 9, ProductId = 2, Size = "44", Quantity = 2 },
    new ProductSize { Id = 10, ProductId = 2, Size = "45", Quantity = 2 },


    new ProductSize { Id = 11, ProductId = 3, Size = "41", Quantity = 6 },
    new ProductSize { Id = 12, ProductId = 3, Size = "42", Quantity = 6 },
    new ProductSize { Id = 13, ProductId = 3, Size = "43", Quantity = 5 },
    new ProductSize { Id = 14, ProductId = 3, Size = "44", Quantity = 4 },
    new ProductSize { Id = 15, ProductId = 3, Size = "45", Quantity = 4 },

    new ProductSize { Id = 16, ProductId = 4, Size = "41", Quantity = 8 },
    new ProductSize { Id = 17, ProductId = 4, Size = "42", Quantity = 7 },
    new ProductSize { Id = 18, ProductId = 4, Size = "43", Quantity = 6 },
    new ProductSize { Id = 19, ProductId = 4, Size = "44", Quantity = 5 },
    new ProductSize { Id = 20, ProductId = 4, Size = "45", Quantity = 4 },


    new ProductSize { Id = 21, ProductId = 5, Size = "S", Quantity = 10 },
    new ProductSize { Id = 22, ProductId = 5, Size = "M", Quantity = 12 },
    new ProductSize { Id = 23, ProductId = 5, Size = "L", Quantity = 10 },
    new ProductSize { Id = 24, ProductId = 5, Size = "XL", Quantity = 8 },

    new ProductSize { Id = 25, ProductId = 6, Size = "41", Quantity = 4 },
    new ProductSize { Id = 26, ProductId = 6, Size = "42", Quantity = 4 },
    new ProductSize { Id = 27, ProductId = 6, Size = "43", Quantity = 4 },
    new ProductSize { Id = 28, ProductId = 6, Size = "44", Quantity = 3 },
    new ProductSize { Id = 29, ProductId = 6, Size = "45", Quantity = 3 },

    new ProductSize { Id = 30, ProductId = 7, Size = "41", Quantity = 4 },
    new ProductSize { Id = 31, ProductId = 7, Size = "42", Quantity = 5 },
    new ProductSize { Id = 32, ProductId = 7, Size = "43", Quantity = 5 },
    new ProductSize { Id = 33, ProductId = 7, Size = "44", Quantity = 4 },
    new ProductSize { Id = 34, ProductId = 7, Size = "45", Quantity = 4 },

    new ProductSize { Id = 35, ProductId = 8, Size = "7", Quantity = 25 },
    new ProductSize { Id = 36, ProductId = 9, Size = "7", Quantity = 20 },


    new ProductSize { Id = 37, ProductId = 10, Size = "41", Quantity = 3 },
    new ProductSize { Id = 38, ProductId = 10, Size = "42", Quantity = 4 },
    new ProductSize { Id = 39, ProductId = 10, Size = "43", Quantity = 4 },
    new ProductSize { Id = 40, ProductId = 10, Size = "44", Quantity = 3 },
    new ProductSize { Id = 41, ProductId = 10, Size = "45", Quantity = 2 },

    new ProductSize { Id = 42, ProductId = 11, Size = "41", Quantity = 3 },
    new ProductSize { Id = 43, ProductId = 11, Size = "42", Quantity = 4 },
    new ProductSize { Id = 44, ProductId = 11, Size = "43", Quantity = 4 },
    new ProductSize { Id = 45, ProductId = 11, Size = "44", Quantity = 2 },
    new ProductSize { Id = 46, ProductId = 11, Size = "45", Quantity = 1 },


    new ProductSize { Id = 47, ProductId = 12, Size = "2–24 кг", Quantity = 10 },


    new ProductSize { Id = 48, ProductId = 13, Size = "102 in²", Quantity = 12 },

    new ProductSize { Id = 49, ProductId = 14, Size = "Стандартни", Quantity = 60 },

    new ProductSize { Id = 50, ProductId = 15, Size = "33", Quantity = 6 },
    new ProductSize { Id = 51, ProductId = 15, Size = "34", Quantity = 6 },
    new ProductSize { Id = 52, ProductId = 15, Size = "35", Quantity = 7 },
    new ProductSize { Id = 53, ProductId = 15, Size = "36", Quantity = 6 },

    new ProductSize { Id = 54, ProductId = 16, Size = "33", Quantity = 5 },
    new ProductSize { Id = 55, ProductId = 16, Size = "34", Quantity = 5 },
    new ProductSize { Id = 56, ProductId = 16, Size = "35", Quantity = 6 },
    new ProductSize { Id = 57, ProductId = 16, Size = "36", Quantity = 6 },


    new ProductSize { Id = 58, ProductId = 18, Size = "Малка", Quantity = 7 },
    new ProductSize { Id = 59, ProductId = 18, Size = "Голяма", Quantity = 8 },
    new ProductSize
    {
        Id = 60,
        ProductId = 17,
        Size = "Един размер",
        Quantity = 18 // = Stock
    },
    new ProductSize
    {
        Id = 61,
        ProductId = 19,
        Size = "Един размер",
        Quantity = 35 // = Stock
    },
    new ProductSize
    {
        Id = 62,
        ProductId = 20,
        Size = "Стандартна",
        Quantity = 28 // = Stock
    },
    new ProductSize { Id = 63, ProductId = 21, Size = "41", Quantity = 4 },
new ProductSize { Id = 64, ProductId = 21, Size = "42", Quantity = 6 },
new ProductSize { Id = 65, ProductId = 21, Size = "43", Quantity = 5 },
new ProductSize { Id = 66, ProductId = 21, Size = "44", Quantity = 3 },
new ProductSize { Id = 67, ProductId = 21, Size = "45", Quantity = 3 },
new ProductSize { Id = 68, ProductId = 22, Size = "41", Quantity = 5 },
new ProductSize { Id = 69, ProductId = 22, Size = "42", Quantity = 5 },
new ProductSize { Id = 70, ProductId = 22, Size = "43", Quantity = 4 },
new ProductSize { Id = 71, ProductId = 22, Size = "44", Quantity = 3 },
new ProductSize { Id = 72, ProductId = 22, Size = "45", Quantity = 3 },
new ProductSize { Id = 73, ProductId = 23, Size = "41", Quantity = 4 },
new ProductSize { Id = 74, ProductId = 23, Size = "42", Quantity = 4 },
new ProductSize { Id = 75, ProductId = 23, Size = "43", Quantity = 4 },
new ProductSize { Id = 76, ProductId = 23, Size = "44", Quantity = 2 },
new ProductSize { Id = 77, ProductId = 23, Size = "45", Quantity = 1 },
new ProductSize { Id = 78, ProductId = 24, Size = "41", Quantity = 2 },
new ProductSize { Id = 79, ProductId = 24, Size = "42", Quantity = 3 },
new ProductSize { Id = 80, ProductId = 24, Size = "43", Quantity = 3 },
new ProductSize { Id = 81, ProductId = 24, Size = "44", Quantity = 2 },
new ProductSize { Id = 82, ProductId = 25, Size = "41", Quantity = 3 },
new ProductSize { Id = 83, ProductId = 25, Size = "42", Quantity = 3 },
new ProductSize { Id = 84, ProductId = 25, Size = "43", Quantity = 3 },
new ProductSize { Id = 85, ProductId = 25, Size = "44", Quantity = 3 },
new ProductSize { Id = 86, ProductId = 26, Size = "41", Quantity = 4 },
new ProductSize { Id = 87, ProductId = 26, Size = "42", Quantity = 5 },
new ProductSize { Id = 88, ProductId = 26, Size = "43", Quantity = 5 },
new ProductSize { Id = 89, ProductId = 26, Size = "44", Quantity = 3 },
new ProductSize { Id = 90, ProductId = 27, Size = "Един размер", Quantity = 10 },
new ProductSize { Id = 91, ProductId = 28, Size = "41", Quantity = 4 },
new ProductSize { Id = 92, ProductId = 28, Size = "42", Quantity = 5 },
new ProductSize { Id = 93, ProductId = 28, Size = "43", Quantity = 6 },
new ProductSize { Id = 94, ProductId = 28, Size = "44", Quantity = 4 },
new ProductSize { Id = 95, ProductId = 28, Size = "45", Quantity = 3 },
new ProductSize { Id = 96, ProductId = 29, Size = "41", Quantity = 4 },
new ProductSize { Id = 97, ProductId = 29, Size = "42", Quantity = 5 },
new ProductSize { Id = 98, ProductId = 29, Size = "43", Quantity = 5 },
new ProductSize { Id = 99, ProductId = 29, Size = "44", Quantity = 4 },
new ProductSize { Id = 100, ProductId = 29, Size = "45", Quantity = 2 },
new ProductSize { Id = 101, ProductId = 30, Size = "41", Quantity = 4 },
new ProductSize { Id = 102, ProductId = 30, Size = "42", Quantity = 4 },
new ProductSize { Id = 103, ProductId = 30, Size = "43", Quantity = 4 },
new ProductSize { Id = 104, ProductId = 30, Size = "44", Quantity = 2 },
new ProductSize { Id = 105, ProductId = 30, Size = "45", Quantity = 2 },
new ProductSize { Id = 106, ProductId = 31, Size = "41", Quantity = 3 },
new ProductSize { Id = 107, ProductId = 31, Size = "42", Quantity = 4 },
new ProductSize { Id = 108, ProductId = 31, Size = "43", Quantity = 4 },
new ProductSize { Id = 109, ProductId = 31, Size = "44", Quantity = 3 },
new ProductSize { Id = 110, ProductId = 32, Size = "41", Quantity = 4 },
new ProductSize { Id = 111, ProductId = 32, Size = "42", Quantity = 5 },
new ProductSize { Id = 112, ProductId = 32, Size = "43", Quantity = 4 },
new ProductSize { Id = 113, ProductId = 32, Size = "44", Quantity = 2 },
new ProductSize { Id = 114, ProductId = 33, Size = "41", Quantity = 3 },
new ProductSize { Id = 115, ProductId = 33, Size = "42", Quantity = 4 },
new ProductSize { Id = 116, ProductId = 33, Size = "43", Quantity = 4 },
new ProductSize { Id = 117, ProductId = 33, Size = "44", Quantity = 2 },
new ProductSize { Id = 118, ProductId = 34, Size = "41", Quantity = 3 },
new ProductSize { Id = 119, ProductId = 34, Size = "42", Quantity = 3 },
new ProductSize { Id = 120, ProductId = 34, Size = "43", Quantity = 2 },
new ProductSize { Id = 121, ProductId = 34, Size = "44", Quantity = 2 },
   new ProductSize { Id = 122, ProductId = 35, Size = "41", Quantity = 5 },
    new ProductSize { Id = 123, ProductId = 35, Size = "42", Quantity = 6 },
    new ProductSize { Id = 124, ProductId = 35, Size = "43", Quantity = 5 },
    new ProductSize { Id = 125, ProductId = 35, Size = "44", Quantity = 4 },
    new ProductSize { Id = 126, ProductId = 35, Size = "45", Quantity = 3 },

    new ProductSize { Id = 127, ProductId = 36, Size = "41", Quantity = 6 },
    new ProductSize { Id = 128, ProductId = 36, Size = "42", Quantity = 7 },
    new ProductSize { Id = 129, ProductId = 36, Size = "43", Quantity = 6 },
    new ProductSize { Id = 130, ProductId = 36, Size = "44", Quantity = 5 },
    new ProductSize { Id = 131, ProductId = 36, Size = "45", Quantity = 4 },

    new ProductSize { Id = 132, ProductId = 37, Size = "41", Quantity = 5 },
    new ProductSize { Id = 133, ProductId = 37, Size = "42", Quantity = 6 },
    new ProductSize { Id = 134, ProductId = 37, Size = "43", Quantity = 5 },
    new ProductSize { Id = 135, ProductId = 37, Size = "44", Quantity = 4 },
    new ProductSize { Id = 136, ProductId = 37, Size = "45", Quantity = 4 },

    new ProductSize { Id = 137, ProductId = 38, Size = "41", Quantity = 5 },
    new ProductSize { Id = 138, ProductId = 38, Size = "42", Quantity = 6 },
    new ProductSize { Id = 139, ProductId = 38, Size = "43", Quantity = 5 },
    new ProductSize { Id = 140, ProductId = 38, Size = "44", Quantity = 4 },
    new ProductSize { Id = 141, ProductId = 38, Size = "45", Quantity = 3 },

    new ProductSize { Id = 142, ProductId = 39, Size = "41", Quantity = 6 },
    new ProductSize { Id = 143, ProductId = 39, Size = "42", Quantity = 7 },
    new ProductSize { Id = 144, ProductId = 39, Size = "43", Quantity = 6 },
    new ProductSize { Id = 145, ProductId = 39, Size = "44", Quantity = 5 },
    new ProductSize { Id = 146, ProductId = 39, Size = "45", Quantity = 4 },

    new ProductSize { Id = 147, ProductId = 40, Size = "41", Quantity = 7 },
    new ProductSize { Id = 148, ProductId = 40, Size = "42", Quantity = 8 },
    new ProductSize { Id = 149, ProductId = 40, Size = "43", Quantity = 6 },
    new ProductSize { Id = 150, ProductId = 40, Size = "44", Quantity = 5 },
    new ProductSize { Id = 151, ProductId = 40, Size = "45", Quantity = 4 },

    new ProductSize { Id = 152, ProductId = 41, Size = "41", Quantity = 6 },
    new ProductSize { Id = 153, ProductId = 41, Size = "42", Quantity = 7 },
    new ProductSize { Id = 154, ProductId = 41, Size = "43", Quantity = 5 },
    new ProductSize { Id = 155, ProductId = 41, Size = "44", Quantity = 4 },
    new ProductSize { Id = 156, ProductId = 41, Size = "45", Quantity = 3 },

    new ProductSize { Id = 157, ProductId = 42, Size = "41", Quantity = 5 },
    new ProductSize { Id = 158, ProductId = 42, Size = "42", Quantity = 6 },
    new ProductSize { Id = 159, ProductId = 42, Size = "43", Quantity = 5 },
    new ProductSize { Id = 160, ProductId = 42, Size = "44", Quantity = 4 },
    new ProductSize { Id = 161, ProductId = 42, Size = "45", Quantity = 3 },

    new ProductSize { Id = 162, ProductId = 43, Size = "41", Quantity = 6 },
    new ProductSize { Id = 163, ProductId = 43, Size = "42", Quantity = 7 },
    new ProductSize { Id = 164, ProductId = 43, Size = "43", Quantity = 5 },
    new ProductSize { Id = 165, ProductId = 43, Size = "44", Quantity = 4 },
    new ProductSize { Id = 166, ProductId = 43, Size = "45", Quantity = 3 },

    new ProductSize { Id = 167, ProductId = 44, Size = "41", Quantity = 7 },
    new ProductSize { Id = 168, ProductId = 44, Size = "42", Quantity = 8 },
    new ProductSize { Id = 169, ProductId = 44, Size = "43", Quantity = 6 },
    new ProductSize { Id = 170, ProductId = 44, Size = "44", Quantity = 5 },
    new ProductSize { Id = 171, ProductId = 44, Size = "45", Quantity = 4 },

    new ProductSize { Id = 172, ProductId = 45, Size = "41", Quantity = 6 },
    new ProductSize { Id = 173, ProductId = 45, Size = "42", Quantity = 7 },
    new ProductSize { Id = 174, ProductId = 45, Size = "43", Quantity = 6 },
    new ProductSize { Id = 175, ProductId = 45, Size = "44", Quantity = 5 },
    new ProductSize { Id = 176, ProductId = 45, Size = "45", Quantity = 4 },

    new ProductSize { Id = 177, ProductId = 46, Size = "41", Quantity = 7 },
    new ProductSize { Id = 178, ProductId = 46, Size = "42", Quantity = 8 },
    new ProductSize { Id = 179, ProductId = 46, Size = "43", Quantity = 6 },
    new ProductSize { Id = 180, ProductId = 46, Size = "44", Quantity = 5 },
    new ProductSize { Id = 181, ProductId = 46, Size = "45", Quantity = 4 },

    new ProductSize { Id = 182, ProductId = 47, Size = "41", Quantity = 7 },
    new ProductSize { Id = 183, ProductId = 47, Size = "42", Quantity = 8 },
    new ProductSize { Id = 184, ProductId = 47, Size = "43", Quantity = 6 },
    new ProductSize { Id = 185, ProductId = 47, Size = "44", Quantity = 5 },
    new ProductSize { Id = 186, ProductId = 47, Size = "45", Quantity = 4 },

    new ProductSize { Id = 187, ProductId = 48, Size = "41", Quantity = 7 },
    new ProductSize { Id = 188, ProductId = 48, Size = "42", Quantity = 8 },
    new ProductSize { Id = 189, ProductId = 48, Size = "43", Quantity = 6 },
    new ProductSize { Id = 190, ProductId = 48, Size = "44", Quantity = 5 },
    new ProductSize { Id = 191, ProductId = 48, Size = "45", Quantity = 4 },

    new ProductSize { Id = 192, ProductId = 49, Size = "41", Quantity = 7 },
    new ProductSize { Id = 193, ProductId = 49, Size = "42", Quantity = 8 },
    new ProductSize { Id = 194, ProductId = 49, Size = "43", Quantity = 6 },
    new ProductSize { Id = 195, ProductId = 49, Size = "44", Quantity = 5 },
    new ProductSize { Id = 196, ProductId = 49, Size = "45", Quantity = 4 },

    new ProductSize { Id = 197, ProductId = 50, Size = "41", Quantity = 6 },
    new ProductSize { Id = 198, ProductId = 50, Size = "42", Quantity = 7 },
    new ProductSize { Id = 199, ProductId = 50, Size = "43", Quantity = 6 },
    new ProductSize { Id = 200, ProductId = 50, Size = "44", Quantity = 5 },
    new ProductSize { Id = 201, ProductId = 50, Size = "45", Quantity = 4 },

    new ProductSize { Id = 202, ProductId = 51, Size = "41", Quantity = 6 },
    new ProductSize { Id = 203, ProductId = 51, Size = "42", Quantity = 7 },
    new ProductSize { Id = 204, ProductId = 51, Size = "43", Quantity = 6 },
    new ProductSize { Id = 205, ProductId = 51, Size = "44", Quantity = 5 },
    new ProductSize { Id = 206, ProductId = 51, Size = "45", Quantity = 4 },

    new ProductSize { Id = 207, ProductId = 52, Size = "41", Quantity = 7 },
    new ProductSize { Id = 208, ProductId = 52, Size = "42", Quantity = 8 },
    new ProductSize { Id = 209, ProductId = 52, Size = "43", Quantity = 6 },
    new ProductSize { Id = 210, ProductId = 52, Size = "44", Quantity = 5 },
    new ProductSize { Id = 211, ProductId = 52, Size = "45", Quantity = 4 },

    new ProductSize { Id = 212, ProductId = 53, Size = "41", Quantity = 6 },
    new ProductSize { Id = 213, ProductId = 53, Size = "42", Quantity = 7 },
    new ProductSize { Id = 214, ProductId = 53, Size = "43", Quantity = 6 },
    new ProductSize { Id = 215, ProductId = 53, Size = "44", Quantity = 5 },
    new ProductSize { Id = 216, ProductId = 53, Size = "45", Quantity = 4 },

    new ProductSize { Id = 217, ProductId = 54, Size = "41", Quantity = 6 },
    new ProductSize { Id = 218, ProductId = 54, Size = "42", Quantity = 7 },
    new ProductSize { Id = 219, ProductId = 54, Size = "43", Quantity = 6 },
    new ProductSize { Id = 220, ProductId = 54, Size = "44", Quantity = 5 },
    new ProductSize { Id = 221, ProductId = 54, Size = "45", Quantity = 4 }
);


        }
        public DbSet<EternityX.Models.FavoriteProduct> FavoriteProduct { get; set; } = default!;
    }
}
