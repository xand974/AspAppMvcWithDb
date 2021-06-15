Création d'un site type blog avec base de données

Setup => ASP .NET Core vide => ASP .NET MVC

Etape 1 : setup dossiers pour MVC 
	-Models
	-Views
	-Controllers
Etape 2 : setup dossier pour design pattern MVVM
	-ViewModels

Le but étant de faire passer des données directement dans les views en passant par une class ViewModel,
car le faire avec le model peut devenir rapidement compliqué.

Etape 3 : setup Model

Création de deux classes Post et Creator pour avoir un lien entre les postes crées et les créateurs de celle-ci

Etape 4:

Setup de la logique pour les opérations CRUD des postes en utilisant une interface => par extension, setup de la dependency injection. 