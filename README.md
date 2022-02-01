<div style="align:center"><img src ="https://github.com/NYUGameCenter/Unity-Git-Config/blob/master/NYU_GameCenter_Logo_Formatted_Thin.png"></div>

# Codebase for Advanced Game Programming Spring 2022
- [Git Primer](#git-primer)
- [Setup Instructions](#setup-instructions)
- [Usage and Errors](#usage-and-errors)
- [Troubleshooting](#troubleshooting)
- [Additional Information](#additional-information)

We've put together some git configuration files to cover the majority of Unity/Git use cases. If you set these up at the start, they should prevent your repos from filling up with cruft. These config files ensure that all large files are tracked by git lfs & that your changes are diff'd appropriately, while the pre-commit/post-merge hooks ensure that meta files stay properly in sync. They also insure you against accidentally trying to upload a >100mb file to github, and ending up with a sad unresolvable local repo.

