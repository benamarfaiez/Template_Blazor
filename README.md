# 🚀 MonProjet.Blazor - Template Modulaire MVP (.NET 10)

Un template frontend Blazor moderne, hautement modulaire et strictement typé, conçu pour des applications d'entreprise évolutives.

## 📖 Philosophie et Choix Techniques

Ce template a été bâti autour de convictions fortes pour garantir maintenabilité, testabilité et performance :

- **Architecture MVP (Model-View-Presenter)** : Séparation stricte de la logique métier (Présentateurs) et de l'affichage (Vues).
- **Principes SOLID** : Chaque composant a une responsabilité unique. L'utilisation de la composition et de l'injection de dépendances permet d'étendre le framework sans en modifier le cœur (Open/Closed).
- **Zéro dépendance UI tierce** : Tous les composants (Boutons, DataGrid, Dialog) sont natifs (HTML/CSS/Blazor) avec isolation CSS. Aucun MudBlazor, Radzen ou Bootstrap.
- **Mode de rendu `InteractiveAuto`** : L'application démarre en mode Serveur pour un affichage instantané, puis bascule automatiquement en WebAssembly (WASM) sur le client pour une interactivité sans latence.
- **Gestion rigoureuse du cycle de vie** : Les Présentateurs sont `Scoped` et résolus via `OwningComponentBase` pour éviter les fuites de mémoire en Blazor Server.

---

## 📁 Structure du Projet

```text
MonProjet.sln
├── MonProjet.Server/              # Hôte (Rendu SSR, API, Hébergement WASM)
│   ├── Components/App.razor       # Configuration du rendermode (Auto)
│   └── wwwroot/css/app.css        # Variables CSS globales (Design System)
│
└── MonProjet.Client/              # Cœur de l'application (WASM + Composants)
    ├── Components/
    │   ├── Layout/                # MainLayout, AppNavBar
    │   ├── Pages/                 # Les Vues (Home, Users, Gallery)
    │   └── UI/                    # Bibliothèque interne de composants
    │       ├── BasicInput/        # AppButton, AppTextBox, etc.
    │       ├── Collections/       # AppDataGrid, GridColumn (SOLID), etc.
    │       ├── Layout/            # AppContainer, AppStackPanel, AppSplitGrid
    │       └── ...
    ├── Models/                    # ViewModels et DTOs
    ├── Presenters/                # Logique métier et état (Agnostique de Blazor)
    ├── Services/                  # Services transverses (IExceptionHandler)
    └── View/                      # Configurations UI complexes (ex: HomeGridConfig)
