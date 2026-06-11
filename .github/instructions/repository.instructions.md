---
description: C# version examples organization and test project constraints
applyTo: "**"
---

# Repository Instructions

- Each C# version requires its own isolated test project: `Examples.Features.CSharp[VERSION].Tests` with feature examples in `Features.CSharp[VERSION].Tests` folder. Do not mix versions.
- Test projects must use `RootNamespace: Examples` and `LangVersion` matching the target version. Use xunit with `xunit.v3.mtp-v2` and configure `xunit.runner.json`.
- This repository contains examples and tests only—no production code. All new code must be test-based feature demonstrations within version-specific projects.
- Do not override centralized `Directory.Build.props` settings in individual projects.
- Include README.md in each version project explaining features and documenting breaking changes or special considerations.
