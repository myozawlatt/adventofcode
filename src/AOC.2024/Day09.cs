using static System.Linq.Enumerable;

namespace AOC._2024;

[Puzzle(9, 2024, "Disk Fragmenter")]
internal class Day09 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
    {
        var diskmapSpan = inputLines.AsByteSpan();

        List<int> blocks = [];
        bool file = true;

        for (int i = 0; i < diskmapSpan.Length; i++)
        {
            var current = diskmapSpan[i];
            if (file)
                blocks.AddRange(Repeat(i / 2, current - '0'));
            else
                blocks.AddRange(Repeat(-1, current - '0'));

            file = !file;
        }

        //optimized
        Span<int> blockSpans = stackalloc int[blocks.Count];
        blocks.CopyTo(blockSpans);

        for (int i = 0; i < blockSpans.Length; i++)
        {
            if (blockSpans[i] >= 0)
                continue;

            var last = blockSpans.Length - 1;
            while (blockSpans[last] < 0)
                last--;

            blockSpans[i] = blockSpans[last]; //relocated
            blockSpans = blockSpans[..last]; //removed vacuum
        }

        var checksum = 0L;
        for (int i = 0; i < blockSpans.Length; i++)
            checksum += blockSpans[i] * i;

        return checksum;
    }
    public object SolvePart2(string[] inputLines)
    {
        var diskmapSpan = inputLines.AsByteSpan();

        List<(int Id, int length)> blocks = [];
        bool file = true;

        for (int i = 0; i < diskmapSpan.Length; i++)
        {
            var current = diskmapSpan[i];
            if (file)
                blocks.Add((i / 2, current - '0'));
            else
                blocks.Add((-1, current - '0'));

            file = !file;
        }

        var files = blocks.Where(x => x.Id != -1).OrderByDescending(x => x.Id).ToList();

        foreach (var currentFile in files)
        {
            var idx = blocks.IndexOf(currentFile);

            for (int sp = 0; sp < idx; sp++)
            {
                var currentSpace = blocks[sp];
                if (currentSpace.Id != -1 || currentFile.length > currentSpace.length)
                    continue;

                blocks[sp] = currentFile;
                blocks[idx] = (-1, currentFile.length);

                if (idx + 1 < blocks.Count && blocks[idx + 1].Id == -1) //check next
                {
                    blocks[idx] = (-1, blocks[idx].length + blocks[idx + 1].length);
                    blocks.RemoveAt(idx + 1);
                }
                if (blocks[idx - 1].Id == -1) //check previous
                {
                    blocks[idx - 1] = (-1, blocks[idx].length + blocks[idx - 1].length);
                    blocks.RemoveAt(idx);
                }
                if (currentSpace.length > currentFile.length)
                    blocks.Insert(sp + 1, (-1, currentSpace.length - currentFile.length));

                break;
            }
        }

        var checksum = 0L;
        var index = 0;
        foreach (var (Id, length) in blocks)
        {
            for (int i = 0; i < length; i++)
            {
                if (Id > 0)
                    checksum += index * Id;

                index++;
            }
        }

        return checksum;
    }
}
