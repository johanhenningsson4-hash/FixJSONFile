# CI/CD Documentation

## Overview

This project uses GitHub Actions for Continuous Integration and Continuous Deployment (CI/CD). All workflows are defined in the `.github/workflows/` directory.

---

## 📋 Workflows

### 1. Build and Test
**File:** `.github/workflows/build-and-test.yml`

**Triggers:**
- Push to `main`, `master`, or `develop` branches
- Pull requests to `main`, `master`, or `develop` branches
- Manual dispatch

**What it does:**
- ✅ Checks out the code
- ✅ Sets up MSBuild and NuGet
- ✅ Restores NuGet packages
- ✅ Builds the solution in Release configuration
- ✅ Runs tests (if any exist)
- ✅ Uploads build artifacts (EXE, DLLs, configs)

**Artifacts:** Build outputs retained for 30 days

---

### 2. Release
**File:** `.github/workflows/release.yml`

**Triggers:**
- Push tags matching `v*.*.*` (e.g., `v2.0.0`)
- Manual dispatch with version input

**What it does:**
- ✅ Builds the solution in Release configuration (optimized)
- ✅ Creates a ZIP package with all required files
- ✅ Generates a changelog
- ✅ Creates a GitHub Release with the ZIP file
- ✅ Uploads artifacts

**Output:** `FixJSONFile-v{version}-win-x64.zip`

**How to create a release:**
```bash
# Tag the commit
git tag v2.0.0

# Push the tag
git push origin v2.0.0
```

Or use the GitHub Actions UI to manually trigger with a version number.

---

### 3. Code Quality
**File:** `.github/workflows/code-quality.yml`

**Triggers:**
- Push to `main`, `master`, or `develop` branches
- Pull requests
- Every Monday at 9 AM UTC
- Manual dispatch

**What it does:**
- ✅ Builds with Code Analysis enabled
- ✅ Counts code metrics (files, lines)
- ✅ Scans for TODO/FIXME comments
- ✅ Reports code quality metrics

**Reports:** Available in workflow logs

---

### 4. Dependency Check
**File:** `.github/workflows/dependency-check.yml`

**Triggers:**
- Push to `main` or `master` with package config changes
- Every Sunday at midnight UTC
- Manual dispatch

**What it does:**
- ✅ Lists all NuGet packages
- ✅ Checks for available updates
- ✅ Notes about vulnerability scanning
- ✅ Creates dependency report

**Artifacts:** Dependency report retained for 30 days

---

### 5. Pull Request Labeler
**File:** `.github/workflows/pr-labeler.yml`

**Triggers:**
- Pull requests (opened, updated, reopened)

**What it does:**
- ✅ Automatically labels PRs based on changed files
  - `documentation` - Changed .md files
  - `ui` - Changed Designer.cs or .resx files
  - `core` - Changed core logic files
  - `dependencies` - Changed packages.config or .csproj
  - `ci` - Changed workflow files
  - `config` - Changed config files

---

## 🚀 Usage

### Building on Every Commit

The **Build and Test** workflow automatically runs on every push to main branches:

```bash
git add .
git commit -m "Your changes"
git push
```

Check the Actions tab on GitHub to see build status.

---

### Creating a Release

#### Option 1: Using Git Tags (Recommended)

```bash
# Make sure you're on the main branch
git checkout main
git pull

# Create and push a version tag
git tag v2.0.0
git push origin v2.0.0
```

The Release workflow will automatically:
1. Build the project
2. Create a ZIP package
3. Create a GitHub Release
4. Upload the package

#### Option 2: Manual Trigger

1. Go to **Actions** tab on GitHub
2. Select **Release** workflow
3. Click **Run workflow**
4. Enter version number (e.g., `2.0.0`)
5. Click **Run workflow**

---

### Running Code Quality Checks

Code quality checks run automatically, but you can trigger manually:

1. Go to **Actions** tab
2. Select **Code Quality** workflow
3. Click **Run workflow**

---

### Checking Dependencies

1. Go to **Actions** tab
2. Select **Dependency Check** workflow
3. Click **Run workflow**
4. Download the dependency report artifact

---

## 📊 Status Badges

Add these to your README.md to show workflow status:

```markdown
![Build](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/build-and-test.yml/badge.svg)
![Release](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/release.yml/badge.svg)
![Code Quality](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/code-quality.yml/badge.svg)
```

---

## 🔐 Secrets and Permissions

### Required Secrets
- `GITHUB_TOKEN` - Automatically provided by GitHub Actions

### Required Permissions
The workflows use these permissions:
- **contents: write** - For creating releases (Release workflow)
- **contents: read** - For reading repository content
- **pull-requests: write** - For labeling PRs

These are configured in the workflow files.

---

## 📦 Artifacts

### Build Artifacts
**Retention:** 30 days

**Contains:**
- `FixJSONFile.exe` - Main application
- `.dll` files - Dependencies
- `.config` files - Configuration

**Location:** Actions → Build workflow → Artifacts section

### Release Packages
**Retention:** Permanent (attached to GitHub Releases)

**Contains:**
- All executable and DLL files
- Configuration files
- README.md

**Format:** `FixJSONFile-v{version}-win-x64.zip`

---

## 🛠️ Customization

### Changing Build Configuration

Edit the `env` section in workflows:

```yaml
env:
  BUILD_CONFIGURATION: Release  # Change to Debug if needed
  BUILD_PLATFORM: Any CPU       # Change platform if needed
```

### Adding Test Support

When you add test projects, the **Build and Test** workflow will automatically detect and run them.

Supported test assemblies:
- `*Tests.dll`
- `*Test.dll`

To use a specific test framework, update the workflow:

```yaml
- name: Run tests
  run: |
	vstest.console.exe **/*Tests.dll /Logger:trx
```

### Modifying Release Package Contents

Edit the release workflow's "Create release package" step to include/exclude files:

```yaml
- name: Create release package
  run: |
	# Add your custom files here
	Copy-Item "LICENSE.txt" $releaseDir -Force
	Copy-Item "CHANGELOG.md" $releaseDir -Force
```

---

## 🐛 Troubleshooting

### Build Fails with Missing NuGet Packages

**Solution:** Ensure `packages.config` is committed to the repository.

```bash
git add FixJSONFile/packages.config
git commit -m "Add packages.config"
git push
```

### MSBuild Not Found

**Solution:** The workflow uses `microsoft/setup-msbuild@v2` which should handle this automatically. If issues persist, check the Visual Studio version specification in the workflow.

### Release Not Created After Tagging

**Check:**
1. Tag format must be `v*.*.*` (e.g., `v2.0.0`)
2. Check Actions tab for workflow run status
3. Ensure GITHUB_TOKEN has write permissions

### Artifacts Not Uploaded

**Check:**
1. Build succeeded
2. Output files exist in `bin/Release/` directory
3. File paths in workflow are correct

---

## 📈 Best Practices

### Version Numbers

Use [Semantic Versioning](https://semver.org/):
- **Major:** Breaking changes (v2.0.0)
- **Minor:** New features (v2.1.0)
- **Patch:** Bug fixes (v2.1.1)

### Branch Strategy

Recommended branch structure:
- `main` or `master` - Production-ready code
- `develop` - Development branch
- `feature/*` - Feature branches
- `hotfix/*` - Urgent fixes

### Commit Messages

Use conventional commits:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation
- `refactor:` - Code refactoring
- `test:` - Adding tests
- `ci:` - CI/CD changes

Example:
```bash
git commit -m "feat: add drag and drop support"
git commit -m "fix: resolve file access permission issue"
```

---

## 📝 Workflow Summary Table

| Workflow | Trigger | Purpose | Output |
|----------|---------|---------|--------|
| Build and Test | Push/PR | Validate code | Build artifacts |
| Release | Tag `v*.*.*` | Create release | Release ZIP |
| Code Quality | Push/Schedule | Code analysis | Quality report |
| Dependency Check | Schedule/Manual | Check packages | Dependency report |
| PR Labeler | Pull Request | Auto-label PRs | Labels |

---

## 🔄 Continuous Improvement

Future enhancements to consider:

- [ ] Add unit test project and coverage reporting
- [ ] Integrate code coverage tools (Coverlet)
- [ ] Add security scanning (Snyk, OWASP)
- [ ] Implement automated changelog generation
- [ ] Add performance benchmarking
- [ ] Create pre-release/beta workflows
- [ ] Add integration tests
- [ ] Implement automatic version bumping

---

## 📞 Support

For questions or issues with CI/CD workflows:

1. Check workflow logs in Actions tab
2. Review this documentation
3. Check GitHub Actions documentation
4. Open an issue in the repository

---

**Last Updated:** 2025
**CI/CD Platform:** GitHub Actions
**Project:** FixJSONFile
