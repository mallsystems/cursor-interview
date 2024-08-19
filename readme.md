# Personal Profile Registration
This project is a web application that allows users to register and manage their personal profiles within a Common Data Repository (CDR). The application integrates with the [CountriesNow API](https://countriesnow.space/api/v0.1/countries/) to dynamically populate country and locality information during user registration.

## Features

- **Login & Profile Management:**
  - Users can log in to the application.
  - Upon logging in, users are required to view their personal profile.
  - Users can edit their personal profile using the provided "Edit" button within the profile view.

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
