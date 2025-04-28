![Drift Icon](icon.png) ![Drift Text](text.png)
<p align="center"> <b>Make hard transfers easy — Seamlessly migrate your browser data.</b> </p>
<p align="center"> <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0"> <img alt=".NET 8" src="https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet&logoColor=white"> </a> <a href="https://github.com/babymonie/drift"> <img alt="GitHub Repo" src="https://img.shields.io/badge/GitHub-Drift-black?logo=github"> </a> <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows-blue?logo=windows&logoColor=white"> <img alt="License" src="https://img.shields.io/badge/License-MIT-yellow.svg"> </p>
> **Make hard transfers easy** — Drift seamlessly migrates your browser data without the headache.

---

## 📖 Overview
**Drift** is a Windows Forms application built on **.NET 8** that automates transferring browser data — bookmarks, history, cookies, extensions, and settings — between browsers like Opera GX and Microsoft Edge.  
No more manual file hunting.

---

## ⚙️ Features
- **User-Friendly UI** — Select what and where to transfer
- **Auto-Detect Paths** — Locates default profile folders
- **Manual Selection** — Browse manually if auto-detect fails
- **Admin Permissions** — Ensures proper access for file operations
- **Secure Copy** — Handles bookmarks, history, cookies, extensions, and preferences safely

---

## 🚀 Getting Started

### Requirements
- Windows 10 or higher
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Admin rights (to access browser data)

### Installation
```bash
git clone https://github.com/babymonie/drift.git
cd drift
```
- Open the solution file “Drift.sln” in Visual Studio 2022 or newer.
- Restore NuGet packages.
- Build the solution; then run Drift.exe from either `/bin/Debug` or `/bin/Release`. Alternatively, download the latest version from the [releases page](https://github.com/babymonie/drift/releases) and run Drift.exe directly.

---

## 🧹 Setting up Your Repo

Already have your files locally? Here's how to connect them to GitHub:

```bash
# Navigate to your local drift project folder
cd path/to/your/local/drift

# Initialize Git (if not already initialized)
git init

# Add the remote repository
git remote add origin https://github.com/babymonie/drift.git

# Stage and commit all files
git add .
git commit -m "Initial commit: Add Drift source code"

# Rename the branch to master and push changes
git branch -M master
git push -u origin master
```

(✅ Then, after setting `master` as default, you can delete `main` remotely.)

```bash
git push origin --delete main
```

## 🤝 Contributing
Pull requests are welcome!  
Steps:
1. Fork this repo
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m "Add your feature"`)
4. Push to your branch (`git push origin feature/your-feature`)
5. Open a Pull Request 🚀

---

## 📄 License
Licensed under the **MIT License** — see [`LICENSE`](LICENSE) for details.

---

> Built with ❤️ on .NET 8

---

