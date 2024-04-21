# Solution overview:
The solution consists of 1 project. It uses the .NET 8.0 framework.    

# Solution nuget packages:
- Bogus - to generate test data;
- FluentAssertions - to make assertions; 
- NUnit - to run tests;
- Playwright - to interact with browser;
- System.Drawing.Common - Provides access to GDI+ graphics functionality.

# Projects structure:
- downloads - all tests downloads
- Models - Page Object Models
- Tests - contains tests with their fixtures
- TestsData - static data that is used by tests

# How to execute:
https://github.com/AbyssalWhale/app_form/assets/53709071/dd9b7ac2-d6d1-4064-af6f-0c522cead439

# Issues:
- alert with error messages when trying to submit a form without or invalid first name, last name, and email. I didn't manage to catch the alert since it was missing in the DOM;
- DOM structure should be improved on 'Form Submissions' page
- slider functionality should be improved since it can be easily passed;
- it's not clear regarding minimum and maximum sizes for first name, last name, and email;
- it's not clear regarding the requirements for the avatar:
  - resolution
  - size
  - format
- The solution itself is not perfect. There are areas for improvement.
  - create common reusable assert messages
  - some methods can be reused. It should be moved to global scope
  - move the instance URL and the rest test run data to the .runsettings file

