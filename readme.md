# Personal Profile Registration
This project is a web application that allows users to register and manage their personal profiles within a Common Data Repository (CDR). The application integrates with the [CountriesNow API](https://countriesnow.space/api/v0.1/countries/) to dynamically populate country and locality information during user registration.

## Features

- **Login & Profile Management:**
  - Users can register an account.
  - Users can log in to the application.
  - Upon logging in, users are required to view their personal profile.

- **Sign-Up:**
  - During sign-up, users are required to enter various details, including selecting their country and locality.
  - The country and locality fields are dynamically populated using the [CountriesNow API](https://countriesnow.space/api/v0.1/countries/).
  - When a user selects a country, the locality field is automatically populated with the relevant cities/towns.
  - If no country is selected, the locality field remains hidden.

## Getting Started

### Prerequisites

- [Node.js](https://nodejs.org/)
- [Angular CLI](https://v17.angular.io/cli)
- [Visual Studio Code](https://code.visualstudio.com/)
- A web browser (e.g., Chrome, Firefox)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/mallsystems/cursor-interview.git
   cd cursor-interview

2. Create a new branch with your name:
   
   ##### Before making any changes, create a new branch using your name. This will help keep your work organized and prevent direct commits to the main branch.
   
   ```bash
   git checkout -b your-name-branch
