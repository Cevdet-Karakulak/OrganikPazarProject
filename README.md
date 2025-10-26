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

### 🔹 ML.NET Satış Tahmini  
Sistem, geçmiş 100.000 sipariş verisini analiz ederek **şehir bazlı satış tahmini modeli** oluşturur.  
Model, sipariş sayısı, şehir, kategori ve tarih parametrelerini değerlendirerek **2026’nın ilk üç ayı için öngörü üretir.**

📊 **Adana ili örneği:**  
- 📅 **Ocak 2026** → Tahmini Sipariş: **110**  
- 📅 **Şubat 2026** → Tahmini Sipariş: **100**  
- 📅 **Mart 2026** → Tahmini Sipariş: **113**
---
### 📸 Örnek Ekran Görüntüsü
<img width="1913" height="902" alt="Image" src="https://github.com/user-attachments/assets/f6285492-d5b3-462c-94e8-4920f380d16a" />
---

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
  
---

### 📸 Örnek Ekran Görüntüsü
<img width="1900" height="912" alt="Image" src="https://github.com/user-attachments/assets/e244a85e-0649-49c8-93de-2c1003276d34" />

---

Bu bölüm, **AI destekli içerik üretimi ile ürün tavsiye sistemini** birleştirerek kullanıcıya hem tarif hem alışveriş deneyimi sunar 🚀

---
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
<img width="1914" height="898" alt="Image" src="https://github.com/user-attachments/assets/ed2b09bf-3126-4173-b2ad-f9a78a052f7e" />

---

Bu harita bileşeni, **veri analitiği + coğrafi görselleştirme** birleşimiyle yöneticilere satışları şehir bazlı değerlendirme imkânı sunar 📊
## 🌍 **Çoklu Dil (Multi-Language) Desteği**

- Navbar’da 🇹🇷 / 🇬🇧 / 🇫🇷 bayrak ikonlarıyla anında dil değişimi yapılabilir.  
- Google Translate widget gizlenmiş, sade bir `LanguageSelector` tasarımı kullanılmıştır.  
- Dil değişimi `Session` veya `Cookie` üzerinden hatırlanır.  
- Admin paneli ve kullanıcı arayüzü tamamen **Türkçe**, **İngilizce** ve **Fransızca** dillerine lokalize edilmiştir.  

<img width="1885" height="916" alt="Image" src="https://github.com/user-attachments/assets/24524edc-46ee-40d6-9348-bd344e3a5c05" />
<img width="1897" height="910" alt="Image" src="https://github.com/user-attachments/assets/054ca840-8f31-45c9-9629-2d6b71cd6c40" />
<img width="1900" height="897" alt="Image" src="https://github.com/user-attachments/assets/316b101d-63dc-4718-99b4-0f3b13034f47" />
---

## 📱 **WhatsApp Entegrasyonu**

Kullanıcı veya admin, 📞 ikonuna tıklayarak **WhatsApp Web** üzerinden doğrudan iletişim kurabilir.
<img width="1278" height="813" alt="Image" src="https://github.com/user-attachments/assets/ce637cd8-d4f1-4696-b54c-a25cf60ac072" />
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


<img width="1885" height="916" alt="Image" src="https://github.com/user-attachments/assets/81c203b9-d0c8-4a0f-80c5-285f28a23c1e" />

<img width="1897" height="910" alt="Image" src="https://github.com/user-attachments/assets/0da78eec-71f2-4f4a-8532-53dc85d7a2d0" />

<img width="1900" height="897" alt="Image" src="https://github.com/user-attachments/assets/e4b03e15-b02f-4a0b-89d9-4f11290ba92d" />

<img width="1903" height="905" alt="Image" src="https://github.com/user-attachments/assets/f09f9c1a-fe1a-4408-b751-8ef4f071f5d6" />

<img width="1907" height="898" alt="Image" src="https://github.com/user-attachments/assets/f9480355-d55d-4fb4-ae06-844fc223b005" />

<img width="1914" height="898" alt="Image" src="https://github.com/user-attachments/assets/ed2b09bf-3126-4173-b2ad-f9a78a052f7e" />

<img width="1666" height="501" alt="Image" src="https://github.com/user-attachments/assets/081a18d3-64c8-4518-9d29-1cafad566304" />

<img width="1901" height="916" alt="Image" src="https://github.com/user-attachments/assets/cc33f970-7723-4a0c-855c-46799edcc815" />

<img width="1912" height="887" alt="Image" src="https://github.com/user-attachments/assets/4d3ea7c4-bb81-4f15-80ce-155d94a0ad01" />

<img width="1915" height="908" alt="Image" src="https://github.com/user-attachments/assets/73564f5f-311f-4a86-8e73-c3a0593556ef" />

<img width="1902" height="907" alt="Image" src="https://github.com/user-attachments/assets/664044a8-081a-4e89-9237-eb8b704ccb79" />

<img width="1907" height="909" alt="Image" src="https://github.com/user-attachments/assets/3791cd36-ed5b-4e14-ae56-b4c4f0e27614" />

<img width="1900" height="906" alt="Image" src="https://github.com/user-attachments/assets/77115e5a-94d9-4751-8a01-d81b70027cae" />

<img width="1912" height="899" alt="Image" src="https://github.com/user-attachments/assets/a0a0a25a-08a6-4c18-927a-de36418e4fe6" />

<img width="1908" height="899" alt="Image" src="https://github.com/user-attachments/assets/4a05621c-5770-404d-b5a1-85e5fb2252d6" />

<img width="1916" height="888" alt="Image" src="https://github.com/user-attachments/assets/cf47e9df-c4a9-42cf-9356-84c9e20a7970" />

<img width="1900" height="889" alt="Image" src="https://github.com/user-attachments/assets/343a599c-2e68-4259-a07f-07b12150aeea" />

<img width="1900" height="912" alt="Image" src="https://github.com/user-attachments/assets/e244a85e-0649-49c8-93de-2c1003276d34" />

<img width="1891" height="908" alt="Image" src="https://github.com/user-attachments/assets/d3502a82-a213-4429-88ca-3c2b45fadce4" />

<img width="1912" height="896" alt="Image" src="https://github.com/user-attachments/assets/31423b56-9fb1-4e52-aed7-e928151cd1fc" />

<img width="1917" height="905" alt="Image" src="https://github.com/user-attachments/assets/21757659-6261-4a6f-80aa-c40c5a1e65ee" />

<img width="1908" height="910" alt="Image" src="https://github.com/user-attachments/assets/2700546d-ff94-44ef-9264-7edf9e12ae9f" />

<img width="1911" height="903" alt="Image" src="https://github.com/user-attachments/assets/5d5c1f84-f0e0-431d-a73b-4e9fe90e39dd" />

<img width="1916" height="907" alt="Image" src="https://github.com/user-attachments/assets/12139264-1712-485d-8c9a-9f893f028007" />

<img width="1917" height="914" alt="Image" src="https://github.com/user-attachments/assets/a99267fc-7b61-4fb7-bd35-19fba981ce46" />

<img width="1913" height="902" alt="Image" src="https://github.com/user-attachments/assets/f6285492-d5b3-462c-94e8-4920f380d16a" />

<img width="1910" height="946" alt="Image" src="https://github.com/user-attachments/assets/74878044-f67d-4550-a3a5-c6cc94ab6cc1" />

<img width="1905" height="908" alt="Image" src="https://github.com/user-attachments/assets/2dfa00fd-4a7d-43de-819b-c658b9fe1a43" />

<img width="1905" height="914" alt="Image" src="https://github.com/user-attachments/assets/23ec1f0a-512d-4a98-86d3-9ef9b632d8fe" />

<img width="1898" height="909" alt="Image" src="https://github.com/user-attachments/assets/aae3f124-42fd-459b-9c75-90d879819978" />

<img width="1886" height="914" alt="Image" src="https://github.com/user-attachments/assets/f7d38b0a-6e15-435f-ad51-2674aabadb97" />

<img width="1901" height="904" alt="Image" src="https://github.com/user-attachments/assets/09f80bed-3646-4815-b6bc-b7a34cde9b7c" />

<img width="1900" height="912" alt="Image" src="https://github.com/user-attachments/assets/8ecbc6e4-8773-4cc2-91fd-f1b094416f66" />

<img width="1278" height="813" alt="Image" src="https://github.com/user-attachments/assets/1837efef-b4d3-4f33-9001-a06342651bd3" />

<img width="1907" height="917" alt="Image" src="https://github.com/user-attachments/assets/fcbd9b22-872d-4f5d-9422-8fecbaf7496c" />

<img width="1902" height="906" alt="Image" src="https://github.com/user-attachments/assets/efdcc8cd-4926-4a85-b372-92e33631ce12" />

<img width="1895" height="906" alt="Image" src="https://github.com/user-attachments/assets/492a5836-1cb9-4ed8-9931-701d8339d47b" />

<img width="1656" height="895" alt="Image" src="https://github.com/user-attachments/assets/9cbb0cd2-5376-489b-ae35-fa102f5aac57" />

