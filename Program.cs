using System;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp1
{
    public class  bowling
    {
        public int roll (Queue<int> queue)
        {
            try
            {
                // This mutates the queue!
                return queue.Dequeue();
            } catch (InvalidOperationException)
            {
                return 0; // Defensive coding, but input validation explicitly out of scope
            }
        }
        public int nextroll(Queue<int> queue)
        {
            try
            {
                // This does not mutate the queue
                return queue.Peek();
            }
            catch (InvalidOperationException)
            {
                return 0; // See "defensive coding" comment above
            }
        }
        public int nextbutoneroll(Queue<int> queue)
        {
            try
            {
                // This does not mutate the queue argument
                Queue<int> q = new Queue<int>(queue);
                q.Dequeue();
                return q.Peek();
            }
            catch (InvalidOperationException)
            {
                return 0; // See "defensive coding" comment above
            }
        }
        public int score(int frame, int runningtotal, Queue<int> queue)
        {
            int f;
            if (frame == 0) return runningtotal;
            else {
                f = roll(queue);
                if (f == 10)
                {
                    f += nextroll(queue) + nextbutoneroll(queue);
                }
                else
                {
                    f += roll(queue);
                    if (f == 10)
                    {
                        f += nextroll(queue);
                    }
                }
            }
            runningtotal += f;
            return score(frame - 1, runningtotal, queue);
        }
        // To save time, if the int[] is too short to represent a real
        // game, the code pads out the remaining frames/rolls with 0s.
        // The test data below were verified using the scoring 
        // calculator at www.bowlinggenius.com.
        [Theory]
        [InlineData(0, new int[] { 0, 0 })]
        [InlineData(1, new int[] { 1, 0 })]
        [InlineData(2, new int[] { 1, 1 })]
        [InlineData(3, new int[] { 1, 1, 1 , 0 })]
        [InlineData(10, new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 })]
        [InlineData(20, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(80, new int[] { 2, 6, 3, 5, 2, 6, 3, 5, 2, 6, 3, 5, 5, 3, 6, 2, 5, 3, 6, 2 })]
        [InlineData(80, new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 })]
        [InlineData(110, new int[] { 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10 })]
        [InlineData(150, new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 })]
        [InlineData(300, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 })]
        public void Testbowls(int finalscore, int[] rolls)
        {
            Assert.Equal(finalscore, score(10, 0, new Queue<int>(rolls)));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
