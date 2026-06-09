# BreakoutPC_Controlled — Breakout com Gravidade 🧱

Jogo de Breakout para **desktop Windows** com uma mecânica diferente: a bolinha sofre **gravidade**, exigindo que você a mantenha no ar rebatendo com a barra. Feito em **Windows Forms / .NET 6**.

É a versão desktop do projeto web [MeuJogoWeb](https://github.com/vbarbosa/MeuJogoWeb) (Blazor).

## Como jogar

- Use as **setas ← →** para mover a barra.
- A bolinha cai por gravidade e rebate ao tocar a barra ou as paredes.
- O objetivo é não deixar a bolinha sair pela parte de baixo da tela.

## Stack

- **.NET 6** (`net6.0-windows`)
- **Windows Forms** (`System.Windows.Forms`)
- Loop de jogo a ~60 FPS via `Timer`, renderização com `System.Drawing`

## Rodando localmente

Pré-requisito: [.NET 6 SDK](https://dotnet.microsoft.com/download) no Windows.

```bash
dotnet run
```

## Build / executável

O projeto está configurado para gerar um **executável único e self-contained** para Windows x64:

```bash
dotnet publish -c Release
```

O `.exe` resultante roda sem precisar do .NET instalado na máquina.

## Estrutura

- [Program.cs](Program.cs) — ponto de entrada
- [GameForm.cs](GameForm.cs) — formulário, loop de jogo, física e renderização
