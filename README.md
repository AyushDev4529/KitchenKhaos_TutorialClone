# KitchenChaos Unity Study

This repository documents my step-by-step learning journey
building a Kitchen Chaos–style game in Unity using C#.

## Learning Notes

📁 Learning notes are maintained inside the Unity project:  
[`Assets/DOCS/`](./Assets/DOCS) — lesson-by-lesson markdown notes with diagrams and refactors.


These notes explain design decisions, refactors, and architecture trade-offs.

**Built by AyushDev4529 | Jan–Mar 2026**

KitchenKhaos is a fast-paced 3D cooking game inspired by Code Monkey’s Unity tutorial series. This project was developed as part of my 2026–2027 game dev roadmap to master gameplay systems, UI architecture, and performance optimization.

---

## 🎮 Gameplay Overview

- Control a chef in a busy kitchen
- Cut, cook, and plate ingredients
- Deliver completed recipes to customers
- Beat the clock and earn high scores

---

## 🧠 Systems Implemented (Current)

| System               | Description                                              |
|----------------------|----------------------------------------------------------|
| Player Movement      | CharacterController-based movement with input system     |
| Camera               | Cinemachine follow camera                                |
| Interaction System   | Raycast-based interaction with counters                  |
| Kitchen Objects      | ScriptableObject-driven spawning and ownership model      |
| ClearCounter         | Counter interaction with safe object instantiation        |

---

## 🚧 Systems Planned / In Progress

- UI & HUD (score, timers, menus)
- Customer AI and order system
- Cooking, cutting, and plating workflows
- Game progression and difficulty scaling
- Visual and audio polish

---

## 🛠️ Tech Stack

- **Engine:** Unity 6000.3.1f1 LTS
- **Language:** C# (modular, event-driven)
- **IDE:** Visual Studio Code
- **Version Control:** Git + GitHub Desktop

- **Platform:** PC (WebGL / itch.io planned)

---

