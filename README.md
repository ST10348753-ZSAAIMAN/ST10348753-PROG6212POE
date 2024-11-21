# Contract Monthly Claim System (CMCS)

## Overview

The **Contract Monthly Claim System (CMCS)** is a web-based application developed using ASP.NET Core MVC. It is designed to streamline the process of submitting, approving, and tracking monthly claims for independent contractor lecturers. The system allows lecturers to submit claims, upload supporting documents, and track the status of their claims, while Programme Coordinators and Academic Managers can review and approve these claims.

In Part 2, the prototype has been enhanced with additional features to improve functionality and user experience.

## Project Structure

- **Controllers/HomeController.cs**: Contains all the controller actions for the main functionalities, including claim submission, approval, document upload, claim status tracking, and real-time updates using SignalR.
- **Views**: The views folder contains all the Razor Views (SubmitClaim.cshtml, ApproveClaim.cshtml, UploadDocument.cshtml, ClaimStatus.cshtml) that render the GUI for different user actions.
- **wwwroot/css/styles.css**: Contains custom styles to ensure consistent UI design across all pages.

## Key Features

- **Lecturer Claim Submission**: Lecturers can submit claims by entering the hours worked, hourly rate, and additional notes.
- **Claim Approval**: Programme Coordinators/Academic Managers can approve or reject claims. The approval process updates claim status in real-time.
- **Document Upload**: Lecturers can upload supporting documents related to their claims. The system validates file size and type.
- **Claim Status Tracking**: The current status of claims (Pending, Approved, Rejected) is displayed. Real-time updates are enabled using SignalR.
- **Error Handling and User Feedback**: Meaningful error messages are displayed to users in case of submission or upload failures.
- **Unit Testing**: Unit tests have been implemented to ensure code quality and to cover key functionalities.

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE
2. **Open the Solution in Visual Studio**:
 **Navigate to the project folder** and open the `.sln` file in Microsoft Visual Studio 2022.

3. **Install Required NuGet Packages**:
**Make sure to install any required NuGet packages**, such as `Moq` for testing and `Microsoft.AspNetCore.SignalR` for real-time updates.

4. **Build the Solution**:
**Go to the Build menu** and select **Rebuild Solution**.

5. **Run the Project**:
 **Press F5** or click the **Run** button in Visual Studio to start the project.
   - The application will open in your default browser.

6. **Navigate to the Application**:
**Default route**: `/Home/HomePage` (You can set any action as the default in `Program.cs`).

---

## Assumptions and Constraints

- **User Roles**: The system assumes two main roles – Lecturers and Programme Coordinators/Academic Managers.
- **Document Upload**: The document upload feature is limited to common file formats such as `.pdf`, `.docx`, and `.xlsx`.
- **Real-Time Updates**: Real-time updates are enabled for claim status changes using SignalR.
- **Prototype Stage**: The project remains a prototype; no functional back-end database is implemented for storing claims and documents.
- **Security**: Authentication and authorization are not implemented in this prototype.

---

## Database Design

The database structure follows a relational model, with three main tables:

- **Lecturer**: Stores lecturer information (e.g., LecturerID, Name, HourlyRate).
- **Claim**: Stores claims associated with lecturers (e.g., ClaimID, HoursWorked, Status).
- **Document**: Stores uploaded documents for each claim (e.g., DocumentID, FilePath).

---

## GUI Layout

The GUI is designed using ASP.NET Core MVC with the following views:

- **Submit Claim**: Allows lecturers to submit claims with relevant details.
- **Approve Claim**: Allows Programme Coordinators to approve or reject claims.
- **Upload Document**: Allows lecturers to upload supporting documents.
- **Claim Status**: Displays the status of a lecturer's claims, including real-time updates.

---

## New Features and Enhancements

- **Real-Time Updates Using SignalR**: Added SignalR to support real-time updates for claim status changes.
- **Unit Testing**: Implemented unit tests for key features using xUnit and Moq.
- **Improved Error Handling**: Enhanced error handling for file uploads and form submissions.
- **Consistent UI Design**: Updated UI styles for a more user-friendly and consistent look across all views.

---

## References

- https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
- https://getbootstrap.com/docs/5.1/getting-started/introduction/
- https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/dotnet/standard/class-libraries
- https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
- https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-6.0
- https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
- https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0

---

## Version Control

All changes and progress are version-controlled through GitHub with regular commits, documenting the development process. The repository can be found at: [https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE](https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE)