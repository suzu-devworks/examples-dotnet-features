# AGENTS

## Purpose

- Existing implementations are preserved as learning history.
- Improvements may be added when better approaches are discovered.

## Project Structure

- Application source code, libraries, and tests are located under the `/src` directory.

## Architecture

- Learning samples are primarily implemented as test code and verified using a test runner.
- If a scenario cannot be represented via test code, the code is created in a separate project, such as a console application.
- Small-scale learning samples may sometimes be implemented directly within the test project.
- While the latest .NET SDK is used, projects are separated based on the `LangVersion`.

## Design Principles

- Use English for all source code, comments, and documentation.
- Preserve existing conventions.
- Prefer simple implementations.

## Workflow

- Keep diffs minimal and reviewable.
- For complex, ambiguous, or high-impact tasks, align on the approach before making substantial changes.
- Do not add or update dependencies without confirmation.
- Ask before breaking changes.
- Validate changes after editing.

## Boundaries

- Do not create pull requests or perform remote repository operations unless instructed.

## References

Consult the relevant instruction or skill for language-specific rules, documentation, testing, or repository-specific workflows.
