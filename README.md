Proje çok katmanlı mimamari temelleri dikkate alınarak oluşturulmuştur.
Bu kapsamda yeniden kullanılabilirlik, yeni özelliklerin kolay uyarlanması, kırılgan olmayan bir yapı kurgulanmıştır.
Uygulama cleanr code çerçevesi altında ve SOLID presipleri dikkate alınarak oluşturulmuştur.

Uygulamada veri katmanında repository deseni unit of work ile beraber uyarlanmıştır.
Ancak burada repository pattern en optimum kullanım için optimize edilerek uyarlanmıştır.
Olası tüm çağrımlar en az metod oluşturulacak şekilde yapılmıştır.
Unit of work ile de tüm yapılan işlemlerin tek bir seferde veritabanına kaydedilmesi sağlanmıştır.
Dikkat edilecek olursa birçok repositoryde ek bir metoda dahi ihtiyaç olmadan işlemler yapılabilmektedir.
Repository pattern geriye IQueryable liste döndüğünden, dönen liste üzerinde sorgulama devam edebilmektedir.
Böylece sorgunun son haline ulaşıldıktan sonra dbye gidilmesi sağlanmıştır.
Service katmanındaki Queries klasörü altında dbset nesneleri için extensionlar hazırlanmıştır.
Bu şekilde yine daha az satır kod ve hataya daha kapalı sorgular oluşturulması amaçlanmıştır.
Buradaki bir sorguda yapılacak bir değişikliğin ilgili tüm sorgular için tek bir yerden yapılması sağlanmıştır.

Uygulamada action status kodları hem uygulama genelinde hem de action bazlı tanımlanmıştır.
Dependency injection ile business ve helper sınıflarına erişim sağlanmıştır.

Swagger uyarlaması yapılmıştır, böylece hem test hem de dökümantasyon sağlanmıştır.

AutoMapper ile db nesneleri son kullanıcıya yönelik olan modellere çevrilmiştir.

Projede exception middleware tanımlanmıştır. Böylece tüm hatalar merkezi bir noktadan yakalanıp gerektiğinde son kullanıcıya daha anlamlı mesajların dönmesi sağlanmıştır.

Memory cache uyarlaması yapılmıştır. Böylece get işlemlerinde çok daha performanslı sonuçlar alınması sağlanmıştır.
Cache resetleme işlemi oluşturulan attribute ile actionlarda tanımlanmıştır.

Action bazlı performans loglaması için TimerAction isimli attribute tanımlaması yapılmıştır.
Bu attribute hangi actionda kullanılırsa o action ile ilgili performans loglaması yapılabilir.

Entity validation için bir filtre tanımlaması yapılmıştır.
Bu filtre ile Product,Category gibi entitylerin db de var olup olmadıkları kontrol edilebilir.

Api endpointleri için integration testleri yazılmıştır. Hem success hem de fail durumlarına ilişkin senaryolar oluşturulmuştur.

Performans testleri için örnek bir benchmark çalışması yapılmıştır.
GetProducts metodunun hem EF hem de cache üzerinden karşılaştırması yapılmıştır.

Db scriptleri , örnek data scripleri ve örnek db backupı projeye eklenmiştir.