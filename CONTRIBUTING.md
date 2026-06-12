# Contributing to FixJSONFile

Thank you for your interest in contributing to FixJSONFile! This document provides guidelines and instructions for contributing.

## Table of Contents
- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [Making Changes](#making-changes)
- [Submitting Changes](#submitting-changes)
- [Coding Standards](#coding-standards)
- [Testing](#testing)
- [Documentation](#documentation)

---

## Code of Conduct

By participating in this project, you agree to maintain a respectful and collaborative environment.

### Our Standards
- Be respectful and inclusive
- Accept constructive criticism
- Focus on what's best for the project
- Show empathy towards others

---

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET Framework 4.7.2 SDK
- Git
- GitHub account

### Find an Issue
1. Browse [existing issues](../../issues)
2. Look for issues labeled `good first issue` or `help wanted`
3. Comment on the issue to express interest

### Ask Questions
- Create an issue with the `question` label
- Check existing documentation first

---

## Development Setup

### 1. Fork the Repository
Click the "Fork" button at the top right of the repository page.

### 2. Clone Your Fork
```bash
git clone https://github.com/johanhenningsson4-hash/FixJSONFile.git
cd FixJSONFile
```

### 3. Add Upstream Remote
```bash
git remote add upstream https://github.com/johanhenningsson4-hash/FixJSONFile.git
```

### 4. Open in Visual Studio
1. Open `FixJSONFile.sln` in Visual Studio
2. Restore NuGet packages (automatic)
3. Build the solution (F6)
4. Run the application (F5)

---

## Making Changes

### 1. Create a Branch
```bash
git checkout -b feature/your-feature-name
```

Branch naming conventions:
- `feature/` - New features
- `fix/` - Bug fixes
- `docs/` - Documentation changes
- `refactor/` - Code refactoring
- `test/` - Adding tests

### 2. Make Your Changes
- Follow the [Coding Standards](#coding-standards)
- Keep changes focused and atomic
- Write clear, descriptive commit messages

### 3. Commit Your Changes
```bash
git add .
git commit -m "feat: add descriptive message"
```

Commit message format:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation
- `refactor:` - Code refactoring
- `test:` - Adding tests
- `ci:` - CI/CD changes

### 4. Keep Your Branch Updated
```bash
git fetch upstream
git rebase upstream/main
```

---

## Submitting Changes

### 1. Push Your Changes
```bash
git push origin feature/your-feature-name
```

### 2. Create a Pull Request
1. Go to your fork on GitHub
2. Click "Pull Request"
3. Select your branch
4. Fill out the PR template
5. Submit the PR

### 3. PR Review Process
- Automated checks will run (build, tests, code quality)
- Maintainers will review your code
- Address any requested changes
- Once approved, your PR will be merged

---

## Coding Standards

### C# Code Style

Follow the project's existing code style:

#### Naming Conventions
```csharp
// Classes: PascalCase
public class JsonFixer

// Methods: PascalCase
public void FixJsonString()

// Private fields: _camelCase
private readonly JsonFixer _jsonFixer;

// Properties: PascalCase
public string InputPath { get; set; }

// Local variables: camelCase
string inputPath = "";
```

#### Language Standards
- Use .NET Framework 4.7.2 features
- C# 7.3 syntax
- English for all code and comments
- XML documentation for public APIs

#### Code Organization
```csharp
// Order:
// 1. Using statements
// 2. Namespace
// 3. Class/interface definition
// 4. Fields
// 5. Constructors
// 6. Properties
// 7. Methods (public first, then private)
```

#### Best Practices
- Keep methods small and focused
- Use meaningful variable names
- Avoid magic numbers
- Handle exceptions appropriately
- Dispose of resources properly
- Use `var` when type is obvious

### UI Guidelines
- Follow Windows Forms best practices
- Maintain consistent layout
- Use descriptive control names
- Set appropriate tab order
- Provide keyboard shortcuts
- Show clear error messages

---

## Testing

### Manual Testing
Before submitting a PR:

1. **Build the solution**
   ```
   Clean solution → Rebuild solution
   ```

2. **Test core functionality**
   - Browse and select input file
   - Browse and select output file
   - Fix a valid JSON file
   - Fix a malformed JSON file
   - Test error scenarios

3. **Test error handling**
   - Empty file paths
   - Non-existent files
   - Read-only files
   - Invalid JSON after fixes

4. **UI testing**
   - Status updates work correctly
   - Progress bar appears/disappears
   - Error messages are clear
   - Browse dialogs work properly

### Automated Testing (Future)
When unit tests are added:
```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "TestName"
```

---

## Documentation

### Update Documentation When:
- Adding new features
- Changing existing behavior
- Fixing bugs that affect usage
- Adding configuration options

### Documentation Files to Update:
- `README.md` - Main project documentation
- `CI-CD.md` - CI/CD workflows
- `IMPROVEMENTS.md` - Implementation notes
- Code comments - Complex logic
- XML documentation - Public APIs

### Documentation Style
- Clear and concise
- Use examples
- Keep it up-to-date
- Proper markdown formatting

---

## Project Structure

```
FixJSONFile/
├── .github/
│   ├── workflows/          # CI/CD workflows
│   ├── ISSUE_TEMPLATE/     # Issue templates
│   ├── pull_request_template.md
│   └── labeler.yml
├── FixJSONFile/
│   ├── Form1.cs            # Main UI form
│   ├── Form1.Designer.cs   # UI designer code
│   ├── JsonFixer.cs        # Business logic
│   ├── Program.cs          # Entry point
│   └── Properties/
├── README.md
├── CI-CD.md
├── CONTRIBUTING.md
├── IMPROVEMENTS.md
├── BEFORE_AFTER.md
└── .gitignore
```

---

## CI/CD Integration

### Automated Checks
Pull requests trigger:
- Build verification
- Code quality checks
- Dependency checks
- Auto-labeling

### Workflow Files
Located in `.github/workflows/`:
- `build-and-test.yml` - Build on PR/push
- `release.yml` - Create releases
- `code-quality.yml` - Code analysis
- `dependency-check.yml` - Package updates
- `pr-labeler.yml` - Auto-label PRs

See [CI-CD.md](CI-CD.md) for details.

---

## Versioning

We use [Semantic Versioning](https://semver.org/):
- **MAJOR** - Breaking changes
- **MINOR** - New features (backward compatible)
- **PATCH** - Bug fixes

---

## Release Process

Maintainers follow this process:

1. Update version in code
2. Update CHANGELOG
3. Create git tag
4. Push tag (triggers release workflow)
5. Verify GitHub Release

---

## Getting Help

### Questions?
- Check existing documentation
- Search closed issues
- Create a new issue with `question` label

### Need Clarification?
- Comment on the relevant issue
- Ask in your pull request
- Tag maintainers if urgent

---

## Recognition

Contributors will be:
- Listed in release notes
- Acknowledged in the README (future)
- Credited in commit history

---

## License

By contributing, you agree that your contributions will be licensed under the same license as the project.

---

## Thank You!

Your contributions make this project better for everyone. Thank you for taking the time to contribute! 🎉
