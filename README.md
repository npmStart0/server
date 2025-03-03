# High-Tech Forum Server

### Application Description:
The High-Tech Forum Server project is a robust server-side application designed to provide services tailored for junior professionals in the high-tech industry. It offers a platform for forums and discussions catering to the junior professional community in the tech world.

### Download and Install:

#### Prerequisites:
 - Node.js
 - MySQL
 - Visual Studio and .NET 8

#### Installation Steps:

1. Clone the repository
`git clone https://github.com/npmStart0/server.git`

2. Create a `.env.local` file in the root directory of the project containing the database connection string in the following format:
`CLIENT_URL="http://localhost:3000"`
`DB_CONNECTION="server=127.0.0.1;uid=root;pwd=1234;database=npm"`
Ensure you update the content of the following parameters:
- `uid`: username 
- `pwd`: password

3. Open the project in Visual Studio.

4. Creating and updating the database. 
- Navigate to 'View' > 'Other Windows' > 'Package Manager Console'.
- In the window that opens, run the command: `update-database` .
##### Note: The 'Default project' in the 'Package Manager Console' must be set to the DAL project.

5. Set the WebApi project as the startup project:
- In the Solution Explorer, right-click on the WebApi project.
- Choose 'Set as StartUp Project'.


6. Run the application by clicking the appropriate button in Visual Studio.

Follow your system prompts and respond accordingly to complete the installation successfully.