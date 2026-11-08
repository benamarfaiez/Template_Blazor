# Template Blazor

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

Template Blazor est une application web de gestion construite avec **Blazor**, **C# et .NET 10**. Elle associe un client Blazor WebAssembly à un hôte ASP.NET Core et utilise le CSS Isolation pour encapsuler les styles des composants.


## Fonctionnalités clés

- Tableau de bord avec recherche et grille de données.
- Pages de gestion des employés et des utilisateurs, avec listes, statistiques, pagination et vues en cartes.
- Galerie de composants d'interface réutilisables.
- Parcours de création de virement en plusieurs étapes.
- **Thème clair/sombre dynamique** : le thème est appliqué à l'élément `<html>` avec l'attribut `data-theme`, mémorisé localement et initialisé selon les préférences système si aucun choix n'a été enregistré.
- Styles de composants isolés avec les fichiers `.razor.css` et apparence basée sur des Custom Properties CSS.

## Architecture front-end et conventions CSS

### Thème et tokens de design

Le service `ThemeService` pilote le changement de thème à l'aide du module JavaScript `MonProjet.Client/wwwroot/js/theme.js`. Le thème actif est indiqué par `data-theme` sur l'élément `<html>` : les valeurs `light` et `dark` activent les jeux de styles correspondants. Le choix est conservé dans `localStorage` sous la clé `theme` ; à défaut, la préférence système est utilisée.

Les variables et tokens CSS globaux sont définis dans `MonProjet.Server/wwwroot/css/app.css` : `:root` contient le thème clair et `html[data-theme="dark"]` les valeurs du thème sombre. Les styles globaux complémentaires du client se trouvent dans `MonProjet.Client/wwwroot/app.css`.

### Règles CSS pour les contributions

- Dans les fichiers `.razor.css`, utilisez **exclusivement des Custom Properties CSS** pour les couleurs, fonds, bordures et autres valeurs visuelles thémables, par exemple `var(--color-text)` ou `var(--color-surface)`.
- N'ajoutez pas de valeurs de couleur en dur (`#fff`, `rgb(...)`, noms de couleur, etc.) dans les styles isolés. Si un token nécessaire manque, définissez-le dans la feuille globale de tokens et utilisez-le dans le composant.
- Placez les styles propres à un composant dans son fichier `NomDuComposant.razor.css` ; réservez les feuilles globales aux styles et tokens partagés.

## Démarrage rapide

### Prérequis

- SDK **.NET 10**.
- Un IDE compatible Blazor est recommandé, par exemple **Visual Studio Community 2026** ou Visual Studio Code avec les extensions .NET appropriées.

Vérifiez que le SDK est installé :

```powershell
dotnet --list-sdks
```

### Cloner, restaurer et lancer

Depuis un terminal PowerShell :

```powershell
git clone https://github.com/benamarfaiez/Template_Blazor.git
cd Template_Blazor
dotnet restore MonProjet.sln
dotnet watch --project MonProjet.Server/MonProjet.Server.csproj
```

L'application est alors disponible à l'adresse **https://localhost:50205**. Le profil de lancement fournit également **http://localhost:50207**.

Pour lancer sans rechargement automatique :

```powershell
dotnet run --project MonProjet.Server/MonProjet.Server.csproj
```

## Structure du projet

```text
.
├── MonProjet.sln
├── MonProjet.Client/
│   ├── Components/
│   │   ├── Layout/          # Layout principal
│   │   ├── Pages/           # Home, Employee, User, Gallery, Treasury
│   │   └── UI/              # Composants réutilisables (Feedback, Menu, Stepper…)
│   ├── Services/            # Services client, dont ThemeService
│   └── wwwroot/
│       ├── app.css          # Styles globaux du client
│       └── js/theme.js      # Persistance et application du thème
└── MonProjet.Server/
    ├── Components/          # Application hôte et composants serveur
    ├── Properties/          # Profils de lancement
    └── wwwroot/css/app.css  # Tokens globaux et thèmes clair/sombre
```

Les feuilles `*.razor.css` sont placées à côté des composants auxquels elles s'appliquent ; Blazor les compile et les isole automatiquement.
