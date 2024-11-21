# Contract Monthly Claim System (CMCS)

## Overview

The **Contract Monthly Claim System (CMCS)** is a web-based application developed using ASP.NET Core MVC. It is designed to streamline the process of submitting, approving, tracking, and managing monthly claims for independent contractor lecturers. The system empowers lecturers to submit claims, upload supporting documents, and track claim statuses, while Programme Coordinators, Academic Managers, and HR personnel can manage, review, and approve claims efficiently.

**In Part 3**, additional features were implemented, such as automated report generation and HR dashboard functionalities, making the system more robust and user-friendly.

---

## Project Structure

- **Controllers/HomeController.cs**: Contains all the controller actions for the main functionalities, including claim submission, approval, document upload, claim status tracking, HR dashboard, and report generation.
- **Views**: The views folder contains all the Razor Views (e.g., `SubmitClaim.cshtml`, `ApproveClaim.cshtml`, `ClaimStatus.cshtml`, `HRDashboard.cshtml`) that render the GUI for different user actions.
- **Models**: Defines models such as `Claim` and `Lecturer` to manage data.
- **wwwroot/css/styles.css**: Contains custom styles to ensure consistent UI design across all pages.

---

## Key Features

### Lecturer View

- **Claim Submission**: Lecturers can submit claims by entering the hours worked, hourly rate, and additional notes. Supporting document uploads are allowed with file validation for type and size.
- **Real-Time Auto-Calculation**: Total payment (`TotalAmount`) is automatically calculated (`HoursWorked * HourlyRate`) as the lecturer enters data.
- **Error Handling**: Clear error messages for invalid inputs or unsupported files.
- **Claim Status Tracking**: Displays the current status (`Pending`, `Approved`, `Rejected`) of claims using a progress bar for better visualization.

### Coordinator/Manager View

- **Claim Approval**: Programme Coordinators and Academic Managers can review claims and approve or reject them. The status is updated dynamically, and flagged claims are highlighted with reasons.
- **Flagging Automation**: Claims that violate predefined policies (e.g., hours exceeding thresholds) are automatically flagged for review.

### HR View

- **HR Dashboard**: Allows HR personnel to manage lecturers' data and view approved claims.
- **Lecturer Management**: HR users can update lecturer details such as name, contact information, and hourly rate.
- **Automated Report Generation**: HR can generate PDF summaries of approved claims, including lecturer details and total amounts, for payroll processing.

---

## Setup Instructions

### Step 1: Clone the Repository
git clone https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE.git

### Step 2: Open the Solution in Visual Studio
1. Navigate to the project folder.
2. Open the `.sln` file in Microsoft Visual Studio 2022.

### Step 3: Install Required NuGet Packages
1. Ensure all required NuGet packages are installed:
   - **iTextSharp** or **iText7** for PDF generation.
   - Any additional packages specified in the project dependencies.

### Step 4: Build the Solution
1. In Visual Studio, go to the **Build** menu.
2. Select **Rebuild Solution** to ensure all dependencies are resolved and the project builds successfully.

### Step 5: Run the Project
1. Press **F5** or click the **Run** button in Visual Studio to start the application.
2. The application will open in your default browser.
   - Default route: `/Home/Index`.

### Step 6: Test the Application
1. **Submit a Claim**:
   - Use the **Submit a Claim** button to input claim details (hours worked, hourly rate, and supporting documents).
2. **Approve or Reject Claims**:
   - Use the **Approve Claims** button to view and process pending claims.
3. **Access the HR Dashboard**:
   - Use the **HR Dashboard** button to manage approved claims and lecturer details.

---

## HR Dashboard

The **HR Dashboard** provides HR users with the following functionalities:

1. **View Approved Claims**:
   - Lists all approved claims with details such as claim ID, lecturer name, hours worked, and total amount.
2. **Update Lecturer Details**:
   - Editable forms allow HR users to update lecturers' names, contact information, and hourly rates.
3. **Generate PDF Reports**:
   - HR can download summaries of all approved claims for payroll processing.

---

## New Features and Enhancements in Part 3

### HR View Automation
- **Dashboard Interface**:
  - A new HR Dashboard (`HRDashboard.cshtml`) was introduced for managing lecturer data and approved claims.
- **Lecturer Management**:
  - Forms enable HR personnel to update lecturer details efficiently.
- **Report Generation**:
  - Integrated `iText7` to generate PDF reports summarizing approved claims, including payment details.

### Claim and File Management
- **Enhanced File Security**:
  - Uploaded documents are saved with unique filenames to prevent overwriting.
- **File Validation**:
  - Strict validation for file size (maximum 2 MB) and type (`.pdf`, `.docx`, `.xlsx`).

### Additional Improvements
- **UI Consistency**:
  - Updated UI styles across all views for a user-friendly experience.
- **Error Handling**:
  - Comprehensive error handling for file uploads and invalid inputs.
- **Real-Time Feedback**:
  - Progress bars in `ClaimStatus.cshtml` visually show claim approval progress.

---

## Feedback from Part 2 and Implementation in Part 3

### Feedback Summary

1. **Unable to Submit a Claim**:
   - **Implementation**: The `SubmitClaim` functionality was implemented with both client-side and server-side validation. Claims are stored in a static list to simulate a database.

2. **Unable to View Submitted Claims**:
   - **Implementation**: The `ViewSubmittedClaims` action in `HomeController.cs` retrieves all claims and displays them in the `ViewSubmittedClaims.cshtml`.

3. **Upload Button in Claim Submission**:
   - **Implementation**: An upload button was incorporated to allow users to upload supporting documents during claim submission.

4. **Approval/Rejection of Claims**:
   - **Implementation**: The `ApproveClaim` functionality was added, allowing claims to be approved or rejected.

5. **File Size and Type Restrictions for Uploads**:
   - **Implementation**: File size is restricted to 2 MB, and only `.pdf`, `.docx`, and `.xlsx` files are allowed.

6. **Error Messages for Document Uploads**:
   - **Implementation**: Clear error messages are displayed for file size and type issues.

7. **Secure Storage of Uploaded Files**:
   - **Implementation**: Files are saved in `wwwroot/uploads` with unique filenames to prevent overwriting.

8. **Visibility of Uploaded Document Names**:
   - **Implementation**: The uploaded document names are visible and clickable in both the `ApproveClaim` and `ViewSubmittedClaims` views.

---

## Database Design (Future Implementation)

To scale the application, a relational database model can be implemented:

- **Lecturer Table**: 
  - Stores lecturer information (e.g., `LecturerID`, `Name`, `ContactInfo`, `HourlyRate`).
- **Claim Table**:
  - Stores claims data (e.g., `ClaimID`, `HoursWorked`, `TotalAmount`, `Status`, `FlagReason`).
- **Document Table**:
  - Stores paths to uploaded documents (e.g., `DocumentID`, `FilePath`).

---

## Assumptions and Constraints

1. **Prototype Stage**:
   - No back-end database is implemented; a static list simulates database operations.
2. **File Management**:
   - Documents are stored in the `wwwroot/uploads` directory.
3. **User Roles**:
   - HR, Lecturers, Programme Coordinators, and Managers are the assumed roles.
4. **Security**:
   - Authentication and authorization are not implemented in this prototype.

---

## Version Control

All project changes are tracked using GitHub for version control, ensuring transparency and maintaining a record of development progress.

**Repository**: [https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE](https://github.com/ST10348753-ZSAAIMAN/ST10348753-PROG6212POE)

---

## References

- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0)
- [iText PDF Library](https://itextpdf.com/en/products/itext-7)
- [SignalR for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-6.0)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.1/getting-started/introduction/)
- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0)

---

This document captures the updates and features implemented in **Part 3** of the **Contract Monthly Claim System**. It reflects a focus on automation, scalability, and user-friendly interfaces for efficient claim processing and lecturer data management.

