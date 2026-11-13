using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add different priorities with the highest priority last.
    // Expected Result: High is returned first.
    // Defect(s) Found: Expected High but received Medium.
    // The search excluded the final item. Updated the loop boundary.
    // Result after fix: Pending verification.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 4);
        priorityQueue.Enqueue("High", 9);

        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add two items tied for highest priority.
    // Expected Result: First is returned before Second.
    // Defect(s) Found: Expected First but received Second.
    // The >= comparison selected a later item when priorities tied.
    // Changed the comparison to > to preserve FIFO order.
    // Result after fix: Pending verification.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 8);
        priorityQueue.Enqueue("Second", 8);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Remove all items through repeated Dequeue calls.
    // Expected Result: High, Medium, Low, then an empty-queue exception.
    // Defect(s) Found: The second call returned High instead of Medium.
    // Dequeue returned the selected item without removing it.
    // Added removal of the selected item before returning its value.
    // Result after fix: Pending verification.
    public void TestPriorityQueue_RemovesReturnedItems()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("High", 9);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with "The queue is empty."
    // Defect(s) Found: None. This test passed before the fixes.
    // Result after fix: Pending verification.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Add three items with equal priorities.
    // Expected Result: First, Second, Third.
    // Defect(s) Found: Expected First but received Second.
    // The comparison selected later items when priorities tied.
    // Changed the comparison to preserve FIFO order.
    // // Result after fix: Passed.
    public void TestPriorityQueue_EqualPriorities()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("Third", 3);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }
}