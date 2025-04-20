using FakeItEasy;
using MyShoppingList.Application.Commands;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Domain.Entities;
using System.Threading.Tasks;

namespace MyShoppingList.Tests;

public class ItemTests
{
    [Fact]
    public async Task Should_Create_An_Item_In_A_Group()
    {
        // Arrange
        var fakeGroupRepository = A.Fake<IGroupRepository>();

        var fakeItemRepository = A.Fake<IItemRepository>();
        A.CallTo(() => fakeItemRepository.CreateAsync(A<Item>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult(new Item { Id = 1, Name = string.Empty }));
        A.CallTo(() => fakeItemRepository.GetByIdAsync(A<int>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult<Item?>(new Item
            {
                Id = 1,
                Name = string.Empty,
                ItemGroups = [.. Enumerable
                    .Range(0, 1)
                    .Select(id => new ItemGroup { Id = id })]
            }));

        var sut = new CreateItemHandler(fakeGroupRepository, fakeItemRepository);

        // Act
        var item = await sut.HandleAsync(A.Dummy<CreateItemCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEqual(0, item.Id);
        Assert.False(item.Done);
    }

    [Fact]
    public async Task Should_Set_An_Item_In_A_Group_As_Done()
    {
        // Arrange
        var fakeRepository = A.Fake<IItemRepository>();
        A.CallTo(() => fakeRepository.UpdateAsync(A<Item>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult(new Item { Id = 1, Name = string.Empty }));
        A.CallTo(() => fakeRepository.GetByIdAsync(A<int>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult<Item?>(new Item
            {
                Id = 1,
                Name = string.Empty,
                ItemGroups = [.. Enumerable
                    .Range(0, 1)
                    .Select(id => new ItemGroup { Id = id })]
            }));

        var sut = new CompleteItemHandler(fakeRepository);

        // Act
        var item = await sut.HandleAsync(A.Dummy<CompleteItemCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.True(item?.Done);
    }

    [Fact]
    public async Task Should_Set_An_Item_In_A_Group_As_Undone()
    {
        // Arrange
        var fakeRepository = A.Fake<IItemRepository>();
        A.CallTo(() => fakeRepository.UpdateAsync(A<Item>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult(new Item { Id = 1, Name = string.Empty }));
        A.CallTo(() => fakeRepository.GetByIdAsync(A<int>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult<Item?>(new Item
            {
                Id = 1,
                Name = string.Empty,
                ItemGroups = [.. Enumerable
                    .Range(0, 1)
                    .Select(id => new ItemGroup { Id = id })]
            }));

        var sut = new UncompleteItemHandler(fakeRepository);

        // Act
        var item = await sut.HandleAsync(A.Dummy<UncompleteItemCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.False(item?.Done);
    }

    [Fact]
    public async Task Should_Add_An_Item_In_A_Group()
    {
        // Arrange
        var fakeItemRepository = A.Fake<IItemRepository>();
        var fakeGroupRepository = A.Fake<IGroupRepository>();

        var sut = new AddItemInGroupHandler(fakeGroupRepository, fakeItemRepository);

        // Act
        await sut.HandleAsync(A.Dummy<AddItemInGroupCommand>(), TestContext.Current.CancellationToken);

        // Assert
        A.CallTo(() => fakeGroupRepository.UpdateAsync(A<Group>.Ignored, TestContext.Current.CancellationToken))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Should_Remove_An_Item_From_A_Group()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();

        var sut = new RemoveItemFromGroupHandler(fakeRepository);

        // Act
        await sut.HandleAsync(A.Dummy<RemoveItemFromGroupCommand>(), TestContext.Current.CancellationToken);

        // Assert
        A.CallTo(() => fakeRepository.RemoveItemAsync(An<int>.Ignored, An<int>.Ignored, TestContext.Current.CancellationToken))
            .MustHaveHappenedOnceExactly();
    }
}
