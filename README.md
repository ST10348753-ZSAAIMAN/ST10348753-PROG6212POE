# Contract Monthly Claim System (CMCS)

## Overview

The **Contract Monthly Claim System (CMCS)** is a web-based application developed using ASP.NET Core MVC. It is designed to streamline the process of submitting, approving, and tracking monthly claims for independent contractor lecturers. The system allows lecturers to submit claims, upload supporting documents, and track the status of their claims, while Programme Coordinators and Academic Managers can review and approve these claims.

## Project Structure

- **HomeController.cs**: Contains all the controller actions for the main functionalities, including claim submission, approval, document upload, and claim status tracking.
- **Views**: The views folder contains all the Razor Views (SubmitClaim.cshtml, ApproveClaim.cshtml, UploadDocument.cshtml, ClaimStatus.cshtml) that render the GUI for different user actions.
- **wwwroot/css/styles.css**: Contains custom styles to ensure consistent UI design across all pages.

## Key Features

- **Lecturer Claim Submission**: Lecturers can submit claims by entering the hours worked.
- **Claim Approval**: Programme Coordinators/Academic Managers can approve or reject claims.
- **Document Upload**: Lecturers can upload supporting documents related to their claims.
- **Claim Status**: Lecturers can track the current status of their claims.

## Setup Instructions

1. **Clone the Repository**: 
   ```bash
   git clone [https://github.com/yourusername/ST10348753_PROG6212POE.git](https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE)
2. **Open the Solution in Visual Studio**:
- Navigate to the project folder and open the .sln file in Microsoft Visual Studio 2022.
3. **Build the Solution**:
- Go to the Build menu and select Rebuild Solution.
4. **Run the Project**:
- Press F5 or click the Run button in Visual Studio to start the project.
- The application will open in your default browser.
5. **Navigate to the Application**:
- Default route: /Home/SubmitClaim (You can set any action as default in Program.cs).

## Assumptions and Constraints
- **User Roles**: The system assumes two main roles – Lecturers and Programme Coordinators/Academic Managers.
**Document Upload**: Currently, the document upload feature is simulated and does not yet connect to a back-end storage system.
- **Prototype Stage**: The project is a prototype with no functional back-end for storing claims and documents.
- **Security**: No authentication or security features are implemented at this stage.

## Database Design
The database structure follows a relational model, with three main tables:

- **Lecturer**: Stores lecturer information (e.g., LecturerID, Name, HourlyRate).
- **Claim**: Stores claims associated with lecturers (e.g., ClaimID, HoursWorked, Status).
- **Document**: Stores uploaded documents for each claim (e.g., DocumentID, FilePath).
  
## UML Class Diagram
The UML Class Diagram accurately represents the relationships between Lecturer, Claim, and Document, showing a one-to-many relationship between Lecturers and Claims, and a one-to-one relationship between Claims and Documents.

## GUI Layout
The GUI is designed using ASP.NET Core MVC with the following views:

Submit Claim: Allows lecturers to submit claims.
Approve Claim: Allows Programme Coordinators to approve or reject claims.
Upload Document: Allows lecturers to upload supporting documents.
Claim Status: Displays the status of a lecturer’s claims.

## References
Here are the references used throughout the project:

https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0
https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
https://getbootstrap.com/docs/5.1/getting-started/introduction/
https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0
https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
https://learn.microsoft.com/en-us/dotnet/standard/class-libraries
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/

## Version Control
All changes and progress are version-controlled through GitHub with regular commits, documenting the development process. The repository can be found at: [https://github.com/yourusername/ST10348753_PROG6212POE.git](https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE) 
