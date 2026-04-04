using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static int mazeSize = 21;
    static int[,] maze;
    static int playerX, playerY;
    static int playerHP = 3;
    static int attackRange = 1;
    static int stageLevel = 1;
    static int score = 0;

    static List<Enemy> enemies = new List<Enemy>();
    static List<(int x, int y)> items = new List<(int, int)>(); // 아이템 위치 리스트
    static Random rand = new Random();

    class Enemy { public int x, y; }

    static void Main()
    {
        Console.CursorVisible = false;
        StartNewStage();

        while (playerHP > 0)
        {
            Draw();

            if (playerX == mazeSize - 2 && playerY == mazeSize - 2)
            {
                LevelUpBonus();
                stageLevel++;
                StartNewStage();
                continue;
            }

            var key = Console.ReadKey(true).Key;
            int nextX = playerX, nextY = playerY;

            if (key == ConsoleKey.UpArrow) nextY--;
            else if (key == ConsoleKey.DownArrow) nextY++;
            else if (key == ConsoleKey.LeftArrow) nextX--;
            else if (key == ConsoleKey.RightArrow) nextX++;
            else if (key == ConsoleKey.Spacebar) { Attack(); MoveEnemies(); continue; }
            else continue;

            if (CanMove(nextX, nextY))
            {
                playerX = nextX; playerY = nextY;
                CheckCollision(); // 적 및 아이템 체크
                MoveEnemies();
                CheckCollision();
            }
        }
        Console.Clear();
        Console.WriteLine($"\n💀 [GAME OVER] 최종 스테이지: {stageLevel} | 점수: {score}");
    }

    static void StartNewStage()
    {
        GenerateBinaryTreeMaze();
        playerX = 1; playerY = 1;
        enemies.Clear();
        items.Clear();
        InitializeEnemies(5 + (stageLevel * 2));
    }

    static void GenerateBinaryTreeMaze()
    {
        maze = new int[mazeSize, mazeSize];
        for (int y = 0; y < mazeSize; y++)
            for (int x = 0; x < mazeSize; x++) maze[y, x] = 1;

        for (int y = 1; y < mazeSize - 1; y += 2)
        {
            for (int x = 1; x < mazeSize - 1; x += 2)
            {
                maze[y, x] = 0;
                bool canGoRight = (x < mazeSize - 2);
                bool canGoDown = (y < mazeSize - 2);
                if (canGoRight && canGoDown) { if (rand.Next(2) == 0) maze[y, x + 1] = 0; else maze[y + 1, x] = 0; }
                else if (canGoRight) maze[y, x + 1] = 0;
                else if (canGoDown) maze[y + 1, x] = 0;
            }
        }
        maze[mazeSize - 2, mazeSize - 2] = 0;
    }

    static void LevelUpBonus()
    {
        Console.Clear();
        Console.WriteLine($"\n🎊 STAGE {stageLevel} 클리어!");
        Console.WriteLine("보상을 선택하세요: 1.HP+1  2.사거리+1");
        while (true)
        {
            var choice = Console.ReadKey(true).Key;
            if (choice == ConsoleKey.D1) { playerHP++; break; }
            if (choice == ConsoleKey.D2) { attackRange++; break; }
        }
    }

    static void InitializeEnemies(int count)
    {
        while (enemies.Count < count)
        {
            int rx = rand.Next(1, mazeSize - 1), ry = rand.Next(1, mazeSize - 1);
            if (maze[ry, rx] == 0 && (rx > 3 || ry > 3)) enemies.Add(new Enemy { x = rx, y = ry });
        }
    }

    static bool CanMove(int x, int y) => x >= 0 && x < mazeSize && y >= 0 && y < mazeSize && maze[y, x] == 0;

    static void MoveEnemies()
    {
        foreach (var e in enemies)
        {
            if (rand.Next(100) < 30)
            {
                int dx = playerX > e.x ? 1 : (playerX < e.x ? -1 : 0);
                int dy = playerY > e.y ? 1 : (playerY < e.y ? -1 : 0);
                if (CanMove(e.x + dx, e.y)) e.x += dx;
                else if (CanMove(e.x, e.y + dy)) e.y += dy;
            }
        }
    }

    static void CheckCollision()
    {
        // 적 충돌
        var hit = enemies.FirstOrDefault(e => e.x == playerX && e.y == playerY);
        if (hit != null) { playerHP--; enemies.Remove(hit); }

        // 아이템 획득 (하트 아이콘 '♥')
        var item = items.FirstOrDefault(i => i.x == playerX && i.y == playerY);
        if (item != default) { playerHP++; items.Remove(item); score += 50; }
    }

    static void Attack()
    {
        // 제거할 적 리스트
        List<Enemy> toRemove = new List<Enemy>();

        foreach (var e in enemies)
        {
            int dist = Math.Abs(e.x - playerX) + Math.Abs(e.y - playerY);
            // 사거리 안에 있고, 벽에 가려지지 않았는지 체크
            if (dist <= attackRange && IsPathClear(playerX, playerY, e.x, e.y))
            {
                toRemove.Add(e);
                // 30% 확률로 아이템 드랍
                if (rand.Next(100) < 30) items.Add((e.x, e.y));
            }
        }

        score += toRemove.Count * 100;
        foreach (var r in toRemove) enemies.Remove(r);
    }

    // 간단한 벽 체크 로직 (직선상에 벽이 있는지 확인)
    static bool IsPathClear(int x1, int y1, int x2, int y2)
    {
        int currX = x1, currY = y1;
        while (currX != x2 || currY != y2)
        {
            if (currX < x2) currX++; else if (currX > x2) currX--;
            if (currY < y2) currY++; else if (currY > y2) currY--;
            
            if (currX == x2 && currY == y2) return true; // 적에게 도달
            if (maze[currY, currX] == 1) return false;    // 가는 길에 벽이 있음
        }
        return true;
    }

    static void Draw()
    {
        Console.SetCursorPosition(0, 0);
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if (x == playerX && y == playerY) sb.Append("■");
                else if (enemies.Any(e => e.x == x && e.y == y)) sb.Append("▲");
                else if (items.Any(i => i.x == x && i.y == y)) sb.Append("♥"); // 드랍된 아이템
                else if (x == mazeSize - 2 && y == mazeSize - 2) sb.Append("★");
                else sb.Append(maze[y, x] == 1 ? "▩" : "  ");
            }
            sb.AppendLine();
        }
        Console.Write(sb.ToString());
        Console.WriteLine($"\nSTAGE: {stageLevel} | HP: {new string('♥', playerHP)} | ATK Range: {attackRange}");
        Console.WriteLine($"SCORE: {score} | 적 처치 시 30% 확률로 하트(♥) 드랍!");
    }
}
