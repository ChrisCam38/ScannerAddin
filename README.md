# ParameterScanner

**ParameterScanner** is a Revit add-in designed to search for elements within a Revit document based on specific parameter values.
Provides functionality to either isolate or select elements.

## Table of Contents

- Features
- Installation
- Usage
- Files
- Dependencies
  -Contact

## Features

- **Search Elements**: Search for Revit elements based on parameter name and value.
- **Isolate Elements**: Temporarily isolate elements that match the search criteria.
- **Select Elements**: Select elements that match the search criteria.

## Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/ChirsCam38/ParameterScanner.git
   ```

2. **Build the solution** in Visual Studio:

   - Open `ParameterScanner.sln`.
   - Build the solution to generate the `.addin` file and associated binaries.

3. **Copy the Add-in Manifest**:

   - Copy `ParameterScanner.addin` to the Revit Add-ins folder, typically located at:
     ```
     C:\ProgramData\Autodesk\Revit\Addins\2020\
     ```

4. **Copy the Built DLLs**:
   - Ensure the built DLLs are in the appropriate location referenced by the `.addin` file.

## Usage

1. **Launch Revit**.
2. **Navigate to the "Parameters" tab** in the Revit ribbon.
3. **Click on "Parameter Scanner"** to open the main window.
4. **Input Parameter Name and Value**:
   - Enter the parameter name in the "Parameter Name" field.
   - Enter the parameter value in the "Parameter Value" field (optional).
5. **Choose Action**:
   - Click "Isolate in View" to isolate elements.
   - Click "Select" to select elements.

## Files

- **Application.cs**: Entry point for the Revit add-in, manages ribbon setup.
- **UserControl1.xaml / UserControl1.xaml.cs**: Defines the main window UI and its functionality.
- **ParameterScanner.addin**: Add-in manifest file for Revit.
- **Resources**: Folder containing icon image.

## Dependencies

- **Revit API**: Autodesk.Revit.DB, Autodesk.Revit.UI
- **.NET Framework**: Version 4.8
- \*\*Revit 2020

## Contact

- **CHRISTIAN TILANO**
- **Email**: kristiantilano@gmail.com
- **GitHub**: [ChrisCam38](https://github.com/ChrisCam38)
