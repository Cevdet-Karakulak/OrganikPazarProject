<p align="center">
  <img src="https://github.com/cevdetkarakulak/OrganikPazar/blob/main/OrganikPazar_Banner.png?raw=true" alt="Organik Pazar | AI Powered Marketplace Banner" width="100%">
</p>

<h3 align="center">🍏 Organik Pazar – AI Powered Marketplace built with ASP.NET Core 9.0 & PostgreSQL</h3>

---

# 🍏 **Organik Pazar | ASP.NET Core 9.0 + PostgreSQL + AI + ML.NET + SignalR + Multi-Language Gerçek Dünya Projesi** 🚀

**Organik Pazar**, **ASP.NET Core 9.0 MVC** ve **PostgreSQL** teknolojileriyle geliştirilen, **ML.NET** destekli satış tahmini, **Google Gemini** tabanlı tarif öneri sistemi, **SignalR canlı chat** altyapısı, **Leaflet harita analizleri** ve **çoklu dil (TR / EN / FR)** desteği barındıran profesyonel bir tam yığın (Full Stack) projedir.  

Bu proje, bir “organik ürün pazarı”nı uçtan uca modelleyerek hem veritabanı tasarımı hem yapay zekâ hem de gerçek zamanlı etkileşim alanlarında eksiksiz bir uygulama örneği sunar.  

---

## 🎯 **Projenin Amacı**

- 🌱 Organik ürünlere dayalı bir e-ticaret sistemini modern teknolojilerle modellemek  
- 🧠 Kullanıcının elindeki malzemelerden **Google Gemini** ile akıllı yemek tarifleri üretmek  
- 📈 **ML.NET** kullanarak şehir bazlı sipariş verilerinden **2026 yılının ilk üç ayı** için satış tahminleri oluşturmak  
- 🗺️ **Leaflet.js** ile şehir bazlı sipariş yoğunluğunu dinamik harita üzerinde göstermek  
- 💬 **SignalR** ile kullanıcı ↔ admin arasında canlı iletişim sağlamak  
- 🌍 **Multi-language (TR / EN / FR)** altyapısıyla üç dilli kullanıcı deneyimi sunmak  
- 📱 WhatsApp bağlantısı ile kullanıcıya hızlı iletişim imkânı tanımak  

---

## ⚙️ **Kullanılan Teknolojiler**

<p align="center">
  <img src="https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white" />
  <img src="https://img.shields.io/badge/ML.NET-FF6B6B?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Google%20Gemini%20AI-4285F4?style=for-the-badge&logo=google&logoColor=white" />
  <img src="https://img.shields.io/badge/SignalR-0A66C2?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Leaflet%20Map-1C7C54?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Chart.js-FD3A5C?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Multi--Language%20(TR%2FEN%2FFR)-F39C12?style=for-the-badge" />
  <img src="https://img.shields.io/badge/WhatsApp%20Integration-25D366?style=for-the-badge&logo=whatsapp&logoColor=white" />
</p>

---

## 🧠 **Yapay Zekâ & ML.NET Entegrasyonu**


### 🔹 Google Gemini AI  
Kullanıcı elindeki malzemeleri yazdığında sistem, **Google Gemini API** ile doğal dil analizi yapar ve uygun bir yemek tarifi önerir.  
Ayrıca tarif içeriğine göre **site içi ürün önerileri** sunulur:

- Örnek tarif: *“Zeytinyağlı Domatesli Makarna”* 🍝  
  Önerilen ürünler: *Domates (500g)*, *Zeytinyağı (1L)*, *Makarna (500g)*, *Sarımsak (100g)*  
  - 🔗 **Detaya Git:** `/product/{productId}` örn. `/product/42`  
  - 🛒 **Sepete Ekle:** `/cart/add?productId={productId}&qty=1` *(sepet modülü aktif değilse “Sipariş Başlat” veya WhatsApp yönlendirmesi kullanılabilir)*  
  - 💬 **Alternatif (Sepet yoksa):** `https://wa.me/905555555555?text=Merhaba,%20{productName}%20siparişi%20vermek%20istiyorum`

> Öneri motoru; tarifte geçen malzemeleri **SKU/etiket eşleşmesi** ile `product.productname` ve `product.description` alanlarında arar, kategori/benzerlik skoruna göre sıralar.


### 🔹 ML.NET Satış Tahmini  
Sistem, geçmiş 100.000 sipariş verisini analiz ederek **şehir bazlı satış tahmini modeli** oluşturur.  
Model, sipariş sayısı, şehir, kategori ve tarih parametrelerini değerlendirerek **2026’nın ilk üç ayı için öngörü üretir.**

📊 **Adana ili örneği:**  
- 📅 **Ocak 2026** → Tahmini Sipariş: **110**  
- 📅 **Şubat 2026** → Tahmini Sipariş: **100**  
- 📅 **Mart 2026** → Tahmini Sipariş: **113**



## 🧠 **AI Tarif Öneri Sistemi & Ürün Tavsiye Motoru (Google Gemini)**

Sistem, kullanıcının elindeki malzemeleri girdiği prompt üzerinden **Google Gemini API** ile analiz eder, uygun yemek tarifini oluşturur ve bu tarife göre **ürün önerileri** sunar.  

Her öneri; ürün adı, fiyat bilgisi ve işlem butonlarıyla birlikte gelir:

| Ürün | Fiyat | İşlemler |
|------|--------|-----------|
| 🍌 **Muz** | 39,90 ₺ | 🔍 [Detaya Git](#) & 🛒 [Sepete Ekle](#) |
| 🥛 **Badem Sütü** | 49,90 ₺ | 🔍 [Detaya Git](#) & 🛒 [Sepete Ekle](#) |

🧩 Bu yapı, tarifte geçen malzemeleri **Product** tablosundaki ürünlerle eşleştirerek dinamik olarak getirir.  
Örneğin kullanıcı şu girişi yaparsa:

> “Muz, süt ve kakao ile bir tarif öner”  

Gemini API’den dönen tarif:  
> *“Muzlu Kakaolu Smoothie Tarifi”* 🍹  

ve sistem aşağıdaki bileşenleri oluşturur:

- 📦 **Organik Pazar’dan Ürün ve Fiyat Önerileri:** Muz, Süt, Kakao Tozu, Bal  
- 💬 **Alternatif Tarif Önerileri:** “Muzlu Kakaolu Yulaf Lapası”, “Muzlu Puding”, “Donmuş Muzlu Dilimler”  
- 🔗 **Detaya Git / Sepete Ekle butonları:**  
  - `/product/{productId}` → Ürün detay sayfası  
  - `/cart/add?productId={productId}&qty=1` → Sepete ekleme işlemi  

💡 Eğer sepet sistemi devre dışıysa, butonlar otomatik olarak **WhatsApp Sipariş** linkine dönüşür:  
`https://wa.me/905555555555?text=Merhaba,%20{productName}%20siparişi%20vermek%20istiyorum`

---

### 📸 Örnek Ekran Görüntüsü
![AI Tarif Öneri Sistemi](https://raw.githubusercontent.com/cevdetkarakulak/OrganikPazar/main/AI_Recipe_Suggestion.png)

---

Bu bölüm, **AI destekli içerik üretimi ile ürün tavsiye sistemini** birleştirerek kullanıcıya hem tarif hem alışveriş deneyimi sunar 🚀

---

## 🌍 **Çoklu Dil (Multi-Language) Desteği**

- Navbar’da 🇹🇷 / 🇬🇧 / 🇫🇷 bayrak ikonlarıyla anında dil değişimi yapılabilir.  
- Google Translate widget gizlenmiş, sade bir `LanguageSelector` tasarımı kullanılmıştır.  
- Dil değişimi `Session` veya `Cookie` üzerinden hatırlanır.  
- Admin paneli ve kullanıcı arayüzü tamamen **Türkçe**, **İngilizce** ve **Fransızca** dillerine lokalize edilmiştir.  

---

## 📱 **WhatsApp Entegrasyonu**

Kullanıcı veya admin, 📞 ikonuna tıklayarak **WhatsApp Web** üzerinden doğrudan iletişim kurabilir.

```html
<a href="https://wa.me/905555555555" target="_blank">
    <i class="fa fa-whatsapp"></i> WhatsApp ile İletişim
</a>
```

---

## 👨‍💻 **Geliştirici**

**Cevdet Karakulak**  
🧩 Full Stack Developer · AI & Data Enthusiast  
🌐 [LinkedIn](https://www.linkedin.com/in/cevdetkarakulak) | 💻 [GitHub](https://github.com/cevdetkarakulak)

---

## 🪪 **Lisans**

Bu proje **MIT Lisansı** ile paylaşılmıştır.  
Kişisel, eğitim ve portföy amaçlı kullanımlar için serbesttir.  

---

## 🌟 **Teşekkürler**

> M&Y Yazılım Akademi ve Murat Yücedağ’a ilhamları için teşekkür ederim.  
> Organik Pazar, modern .NET teknolojilerinin AI, ML.NET ve çoklu dil desteğiyle birleştiği uçtan uca bir Full Stack başarı örneğidir.



## 🗺️ **Şehir Bazlı Sipariş Yoğunluğu (Leaflet Harita Analizi)**

Proje, **Leaflet.js** kütüphanesini kullanarak Türkiye haritası üzerinde şehir bazlı sipariş analizini görselleştirir.  
Her şehirdeki sipariş yoğunluğu, ortalama sipariş tutarı ve en çok sipariş verilen kategoriye göre dinamik olarak renklendirilir.  

🧩 **Özellikler:**  
- 🔥 Yoğunluğa göre renk ölçeklendirmesi (heat intensity normalization)  
- 📦 Şehir tıklamasıyla detay kutusu: Toplam sipariş, ortalama fiyat, en çok kategori  
- 💬 Tooltip ile şehir ismi ve sipariş sayısı  
- ⚡ API kullanılmadan tamamen **PostgreSQL** sorguları üzerinden veri alınır  

📍 Örnek:  
> Adana → 1.248 sipariş · Ortalama: 527 ₺ · En Popüler Kategori: Meyve  
> İstanbul → 7.832 sipariş · Ortalama: 712 ₺ · En Popüler Kategori: Sebze  

---

### 📸 Örnek Ekran Görüntüsü
![Leaflet Harita Analizi](https://raw.githubusercontent.com/cevdetkarakulak/OrganikPazar/main/Leaflet_Heatmap.png)

---

Bu harita bileşeni, **veri analitiği + coğrafi görselleştirme** birleşimiyle yöneticilere satışları şehir bazlı değerlendirme imkânı sunar 📊
