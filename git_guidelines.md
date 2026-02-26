# Git Workflow & Contribution Guidelines

**Plants vs. Zombies Remake -- MonoGame**

------------------------------------------------------------------------

## 1. Branching Strategy

We use a structured branching model to protect stability and maintain
clean integration.

### Permanent Branches

-   **`main`**
    -   Always stable
    -   Fully playable build
    -   Only merged from `develop`
    -   Tagged for releases
-   **`develop`**
    -   Integration branch
    -   Contains completed features
    -   May contain minor integration bugs
    -   Target branch for all pull requests

------------------------------------------------------------------------

### Temporary Branches

All development happens in feature branches.

Branch types:

feature/`<short-description>`{=html}\
bugfix/`<short-description>`{=html}\
refactor/`<short-description>`{=html}\
hotfix/`<short-description>`{=html}

Examples:

feature/sun-economy\
feature/zombies\
bugfix/projectile-collision\
refactor/grid-manager

Branch naming rules:

-   lowercase only
-   hyphen-separated
-   concise but descriptive
-   no spaces
-   no vague names like `update` or `changes`

------------------------------------------------------------------------

## 2. Standard Development Workflow

### Step 1 -- Update Local Repository

Before starting work:

git checkout develop\
git pull origin develop

Always branch from the latest `develop`.

------------------------------------------------------------------------

### Step 2 -- Create a Feature Branch

git checkout -b feature/plant-shooting

------------------------------------------------------------------------

### Step 3 -- Commit Frequently

Commit small, logical changes.

### Good Commit Format

Add projectile firing system

-   Implemented projectile class\
-   Added cooldown timer\
-   Connected firing to input manager

### Bad Commit Messages

fixed stuff\
update\
changes\
misc

Commit guidelines:

-   Use present tense ("Add", not "Added")
-   Be descriptive
-   Keep commits focused on one concern
-   Avoid committing unrelated changes together

------------------------------------------------------------------------

### Step 4 -- Push Your Branch

git push origin feature/plant-shooting

------------------------------------------------------------------------

### Step 5 -- Open a Pull Request → `develop`

All feature branches merge into `develop`.

PR must include:

-   Description of feature or fix
-   Linked issue
-   Screenshots or video (if visual change)
-   Testing instructions

------------------------------------------------------------------------

## 3. Code Review Requirements

Before merging:

-   Link in pull-requests channel
-   At least 1 approval required
-   Address comments
-   Build must compile
-   No debug logging left behind
-   No commented-out blocks
-   No unrelated file modifications

Do not merge PR without review.

------------------------------------------------------------------------

## 4. Rules & Restrictions

### You MUST NOT:

-   Push directly to `main`
-   Commit directly to `develop`
-   Force push shared branches
-   Mix multiple features in one branch
-   Commit build artifacts
-   Commit secrets or API keys

------------------------------------------------------------------------

## 5. Keeping Your Branch Updated

If `develop` has new changes:

git checkout develop\
git pull origin develop\
git checkout feature/plant-shooting\
git merge develop

Resolve conflicts locally.

Then:

git push

Never ignore merge conflicts.

------------------------------------------------------------------------

## 6. Merge Policy

### Feature Completion

Feature → `develop` when:

-   Feature is complete
-   Compiles successfully
-   Tested locally
-   Reviewed and approved

------------------------------------------------------------------------

### Sprint Completion

`develop` → `main` only when:

-   Sprint goals completed
-   All issues closed
-   Playtested
-   No known major bugs

Tag release:

git tag sprint1\
git push origin sprint1

------------------------------------------------------------------------

## 7. .gitignore Requirements (MonoGame)

The following must NOT be committed:

/bin/\
/obj/\
.vs/\
*.user\
*.suo\
\*.log

If these files appear in PRs, they must be removed before merge.

------------------------------------------------------------------------

## 8. Conflict Resolution Guidelines

When resolving conflicts:

-   Do not blindly accept both changes
-   Understand what each change does
-   Preserve correct logic
-   Rebuild and test after resolution

If unsure, ask in the development channel before merging.

------------------------------------------------------------------------

## 9. Commit Discipline

### Commit Often

Small commits reduce merge conflicts.

### Commit Logical Units

Each commit should represent:

-   A single feature
-   A single bug fix
-   A single refactor

Avoid "mega commits."

------------------------------------------------------------------------

## 10. Clean Code Expectations

All contributions must:

-   Follow C# naming conventions
-   Avoid magic numbers
-   Use constants/config files for balancing
-   Keep classes focused (Single Responsibility Principle)
-   Avoid monolithic "God classes"
-   Avoid unnecessary coupling between systems

------------------------------------------------------------------------

## 11. Emergency Hotfix Workflow

If `main` is broken:

1.  Branch from `main`
2.  Create: hotfix/`<description>`{=html}
3.  Fix issue
4.  PR → `main`
5.  Merge `main` back into `develop`

------------------------------------------------------------------------

## 12. Summary of Core Principles

-   `main` = stable
-   `develop` = integration
-   feature branches = all work
-   PR required for merges
-   Review required
-   Never commit build files
-   Commit small and often
-   Keep branches focused

------------------------------------------------------------------------

## 13. Example Full Workflow

git checkout develop\
git pull\
git checkout -b feature/sun-system

### work...

git add .\
git commit -m "Implement sun generation timer"

git push origin feature/sun-system

### Open PR → develop

### Get review

### Merge
