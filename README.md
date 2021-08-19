De ZorgApp Zapp is een driedelige applicatie met als doel dat zorgmedewerkers efficiënt zorgtaken bij hun clienten kunnen verrichten. Het bestaat uit:
* Een Webapplicatie, waarin een planner bezoeken kan regelen tussen klant en medewerker met een bijbehorend takenpakket.
* Een Mobile app, waarin medewerkers kunnen zien welke bezoeken ze hebben voor die dag en welke handelingen ze moeten verrichten bij dat bezoek.
* Een API, die de communicatie regelt tussen de webapplicatie en de mobile app.

In de eerste twee weken ben ik bezig geweest met het inrichten van de webapplicatie. Uiteindelijk bleek MVC met repositories het juiste design pattern te zijn om te gebruiken hiervoor. Naast simpele CRUD functies zijn er voor twee pagina's wat ingewikkeldere functies bedacht om ze werkend te krijgen. Dit is uiteindelijk goed gelukt. 

## Structuur

---

De webapplicatie bestaat uit diverse pagina's, te weten:

* Inlogscherm.
* Beheer Klanten/Taken/Medewerkers overzicht.
* Bewerk Klant/Taak/Medewerker pagina.
* Bezoekenoverzicht.
* Bezoekdetailpagina.

---
---

## Technologieën

* Microsoft SQL Server
* C#
* Visual Studio
* ASP.NET Core
* Microsoft EntityFrameworkCore
* Bootstrap
* HTML
* CSS
* JavaScript
* JQuery
* Datatables
* FullCalendar
* TavoCalendar
* Design Pattern MVC

Met behulp van design pattern MVC en framework ASP.NET Core is de backoffice gebouwd. De repositories communiceren door middel van Microsoft EntityFramworkCore met de database. De controllers regelen de CRUD operaties van en naar de repositories en verpakken waar nodig de data in een ViewModel gemaakt voor een specifieke pagina. Dit is het geval voor de Edit Klant pagina en het bezoekdetailscherm. In de views wordt gebruikt gemaakt van tag helpers, @helpers, en html helpers voor de communicatie tussen frontend en backend.

## Methoden en Technieken

![ERD](C:\Users\jesse\source\repos\ZorgApp2\ZorgApp2\Read\ERD_ZAPP.png)











