# Sipariş Yönetim Sistemi Backend
Bu proje, Dev Architecture çatısı kullanılarak geliştirilmiş bir backend uygulamasıdır. .NET Core ile yazılmış olan bu sistem, şirket içindeki çalışanlar için bir sipariş yönetim sistemi sağlamaktadır. Kullanıcılar, roller ve sayfalara erişim hakları Dev Architecture ile hazır olarak yapılandırılmıştır. Veritabanı olarak MS SQL kullanılmıştır ve çoklu katmanlı mimari uygulanmıştır.

## Özellikler
Kullanıcı Yönetimi: Kullanıcı bilgileri, roller ve yetkilendirme işlemleri Dev Architecture tarafından sağlanır.
Sipariş Yönetimi: Müşteriler için sipariş oluşturma, güncelleme ve silme işlemleri yapılabilir. Silinen veriler fiziksel olarak veritabanından silinmez, sadece isDeleted kolonu güncellenir.
Ürün ve Depo Yönetimi: Ürünler, renkler ve bedenler gibi bilgiler yönetilebilir.
Raporlama: Depo ve sipariş rapor ekranları ile kullanıcılar sipariş durumlarını takip edebilir.

## Kullanılan Teknolojiler
Backend: .NET 5 / Dev Architecture
Veritabanı: MS SQL
Mimari: Çoklu Katmanlı Mimari
Frontend: Angular (Angular CLI)

## Kurulum
Prerequisites (Ön Koşullar)
Proje çalıştırılmadan önce aşağıdaki yazılımların sisteminizde yüklü olduğundan emin olun:

.NET 5 veya daha yeni bir sürüm İndir
MS SQL Server İndir
Projeyi Çalıştırma
Proje Dosyalarını İndirme:

GitHub üzerinden projeyi klonlayarak veya zip dosyası olarak indirerek başlayabilirsiniz:

git clone https://github.com/kullaniciAdiniz/Siparissistemi-Backend.git
Veritabanı Yapılandırması:

appsettings.json dosyasındaki veritabanı bağlantı ayarlarını düzenleyin.

MS SQL Server'a bağlanacak uygun ConnectionStrings ayarlarını yapın.

json
Kopyala
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=mydb;User Id=myuser;Password=mypassword;"
  }
}
## Projenin Başlatılması:
Projeyi Visual Studio üzerinde açın ve F5 tuşuna basarak çalıştırın.
API'niz https://localhost:5001 adresinde aktif olacaktır.
API Endpoints
Kullanıcı Tanımlama:

POST /api/users
Yeni kullanıcı tanımlaması yapar.
Sipariş Tanımlama:

POST /api/orders
Yeni sipariş kaydı oluşturur.
Ürün Tanımlama:

POST /api/products
Ürün bilgilerini kaydeder.
Renk Tanımlama:

POST /api/colors
Ürün renklerini yönetir.
Depo Raporu:
GET /api/warehouse-report
Depo bilgilerini ve mevcut ürünleri raporlar.
## Veritabanı Yapısı:
Tüm tablolarda aşağıdaki kolonlar yer almalıdır:

Id: Özel anahtar, her tablonun birincil anahtarı.
CreatedUserId: Kaydı oluşturan kullanıcının ID'si.
CreatedDate: Kaydın oluşturulma tarihi.
LastUpdatedUserId: Son güncellemeyi yapan kullanıcının ID'si.
LastUpdatedDate: Kaydın son güncellenme tarihi.
Status: Kaydın aktif olup olmadığı (boolean).
isDeleted: Kaydın silinip silinmediğini gösteren bayrak (boolean).
