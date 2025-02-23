# Code Bundler CLI Application

This repository contains a command-line interface (CLI) application that allows users to bundle code files into a single file. The application supports various programming languages and includes options for sorting, removing empty lines, and adding metadata about the code.

## Features

- Bundle code from specified files into a single output file.
- Support for multiple programming languages: `.cs`, `.py`, `.html`, `.ts`, `.tsx`, `.js`, `.css`, `.cpp`, `.h`, and "all".
- Options to sort the files by name or language.
- Remove empty lines from the output file if desired.
- Include metadata such as the author's name and source code location.
- Create an RSP (Response) file with preset parameters for easier command execution.

## CLI Commands

### 1. Bundle Command
This command bundles the code files into one output file.

bundle --output <file_path> --language <language> --source <true/false> --sort <ab/language> --remove-empty <true/false> --name <author_name>


- `--output` or `-o`: The file name and path for the output.
- `--language` or `-l`: The programming language files to include in the bundle.
- `--source` or `-sk`: Include source code location in the output (default is true).
- `--sort` or `-sr`: How to sort the files (options: "ab", "language", or none).
- `--remove-empty`: Remove empty lines from output (default is true).
- `--name` or `-n`: The name of the writer to include in the output.

### 2. Create RSP Command
This command generates an `rsp` file with the preset parameters for the `bundle` command.

create-rsp

` ## Getting Started ### Prerequisites - .NET 6.0 or higher ### Installation 1. Clone the repository: `bash git clone https://github.com/yourusername/CodeBundlerCLI.git ` 2. Navigate to the project directory: `bash cd CodeBundlerCLI ` 3. Build the application: `bash dotnet build ` 4. Run the application: `bash dotnet run <your_command> ` ## Usage Examples To bundle code files: `bash dotnet run bundle --output outputfile.txt --language .cs --source true --sort ab --remove-empty true --name "Your Name" ` To create an RSP file: `bash dotnet run create-rsp ` This will prompt you for the necessary parameters and create a result.rsp file with your options. ## License This project is licensed under the MIT License. ` ### Summary This README.md provides a comprehensive overview of the CLI application, outlining its features, commands, usage examples, and installation instructions. Adjust any repository links and specific instructions to fit your implementation.


