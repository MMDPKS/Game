Перед запуском игры необходимо указать путь до её элементов на каждой форме (кроме первой).Например,
whiteFigure = new Bitmap(new Bitmap(@"C:\Users\mihai\OneDrive\Desktop\Rabochiy — 2\e.png"), new Size(cellSize - 10, cellSize - 10));
blackFigure = new Bitmap(new Bitmap(@"C:\Users\mihai\OneDrive\Desktop\Rabochiy — 2\k.png"), new Size(cellSize - 10, cellSize - 10));
goldFigure = new Bitmap(new Bitmap(@"C:\Users\mihai\OneDrive\Desktop\Rabochiy — 2\z.png"), new Size(cellSize - 10, cellSize - 10));
Для того чтобы перейти на следующий уровень или перезапустить необходимо растянуть появившееся окно по горизонтали(в право) и нажать на соответствующую кнопку.
