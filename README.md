# Lockr No Webservice

Lockr No Webservice is a Windows Forms application for managing user accounts and their associated data without relying on a web service. This application allows users to register, log in, and manage their accounts securely.

## Features

- User Registration
- User Login
- Account Management
  - Add Account
  - Update Account
  - Delete Account
- Secure storage of user data
- Password and secret key management

## Getting Started

### Prerequisites

- .NET Framework 4.8
- MySQL Database

### Installation

1. Clone the repository:

    ```sh
    git clone https://github.com/iannellilorenzo/lockr-no-webservice.git
    cd lockr-no-webservice
    ```

2. Restore NuGet packages:

    ```sh
    nuget restore lockr-no-webservice.sln
    ```

3. Update the database connection string in [App.config](http://_vscodecontentref_/19):

    ```xml
    <connectionStrings>
        <add name="MySqlConnection" connectionString="server=yourserver;user=youruser;password=yourpassword;database=yourdatabase;" providerName="MySql.Data.MySqlClient" />
    </connectionStrings>
    ```

4. Build the project:

    ```sh
    msbuild lockr-no-webservice.sln
    ```

5. Run the application:

    ```sh
    start bin\Debug\lockr-no-webservice.exe
    ```

## Usage

### Registering a New User

1. Open the application.
2. Click on the "Register" button.
3. Fill in the registration form with your details.
4. Click on the "Register" button to create a new account.

### Logging In

1. Open the application.
2. Enter your email and password.
3. Click on the "Login" button.

### Managing Accounts

1. After logging in, you will be redirected to the Home screen.
2. Use the "Add Account", "Update Account", and "Delete Account" buttons to manage your accounts.

## Important Notes

I couldn't use OpenSSL due to packages not working, so encryption works with AES using a 256 key.

## Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgements

- [BouncyCastle](https://www.bouncycastle.org/csharp/) for cryptographic functions.
- [MySQL](https://www.mysql.com/) for the database.
- [Google Protobuf](https://developers.google.com/protocol-buffers) for serialization.
