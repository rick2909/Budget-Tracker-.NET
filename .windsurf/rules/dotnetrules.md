---
trigger: always_on
description: 
globs: 
---

# Project Structure & Architecture
- The main application logic is in .logic project (Services, Dto Classes, Result classes). This is manneged via EF core, and Linq.
- Database is in Infrastructure (DB context, Models).
- Api and all that is only API and web specific goes in here (Controllers, appsettings.json and program.cs).

# General Code Style & Formatting
- Follow the Microsoft C# code conventions
- Use four spaces for indentation. Don't use tabs.
- Limit lines to 65 characters to enhance code readability on docs, especially on mobile screens.
- Improve clarity and user experience by breaking long statements into multiple lines.
- Use the "Allman" style for braces: open and closing brace its own new line. Braces line up with current indentation level.
- Line breaks should occur before binary operators, if necessary.
- Interface names start with a capital I.
- Attribute types end with the word Attribute.
- Enum types use a singular noun for nonflags, and a plural noun for flags.
- Identifiers shouldn't contain two consecutive underscore (_) characters. Those names are reserved for compiler-generated identifiers.
- Use meaningful and descriptive names for variables, methods, and classes.
- Prefer clarity over brevity.
- Use PascalCase for class names and method names.
- Use camelCase for method parameters and local variables.
- Use PascalCase for constant names, both fields and local constants.
- Private instance fields start with an underscore (_) and the remaining text is camelCased.
- Static fields start with s_. This convention isn't the default Visual Studio behavior, nor part of the Framework design guidelines, but is configurable in editorconfig.
- Avoid using abbreviations or acronyms in names, except for widely known and accepted abbreviations.
- Use meaningful and descriptive namespaces that follow the reverse domain name notation.
- Choose assembly names that represent the primary purpose of the assembly.
- Avoid using single-letter names, except for simple loop counters. Also, syntax examples that describe the syntax of C# constructs often use the following single-letter names that match the convention used in the C# language specification. Syntax examples are an exception to the rule.
  - Use S for structs, C for classes.
  - Use M for methods.
  - Use v for variables, p for parameters.
  - Use r for ref parameters.
