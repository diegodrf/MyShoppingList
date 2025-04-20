using FakeItEasy;
using MyShoppingList.Application.Commands;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Tests;
public class GroupTests
{
    [Fact]
    public async Task Should_Create_A_Group()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();
        A.CallTo(() => fakeRepository.CreateAsync(A<Group>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult(new Group { Id = 1, Name = string.Empty }));

        var sut = new CreateGroupHandler(fakeRepository);

        // Act
        var group = await sut.HandleAsync(A.Dummy<CreateGroupCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEqual(0, group.Id);
    }

    [Fact]
    public async Task When_Search_A_Group_By_Id_And_Exists_Should_Return()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();
        A.CallTo(() => fakeRepository.GetByIdAsync(A<int>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult<Group?>(new Group { Id = 1, Name = string.Empty }));

        var sut = new GetGroupByIdHandler(fakeRepository);

        // Act
        var group = await sut.HandleAsync(A.Dummy<GetGroupByIdCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(group);
    }

    [Fact]
    public async Task When_Search_A_Group_By_Id_And_Not_Exists_Should_Return_Null()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();
        A.CallTo(() => fakeRepository.GetByIdAsync(A<int>.Ignored, TestContext.Current.CancellationToken))
            .Returns(Task.FromResult<Group?>(null));

        var sut = new GetGroupByIdHandler(fakeRepository);

        // Act
        var group = await sut.HandleAsync(A.Dummy<GetGroupByIdCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.Null(group);
    }

    [Fact]
    public async Task When_Has_Groups_Should_Return_All_Groups()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();
        A.CallTo(() => fakeRepository.GetAllAsync(TestContext.Current.CancellationToken))
            .Returns([.. Enumerable.Range(0, 3).Select(x => A.Dummy<Group>())]);

        var sut = new GetAllGroupsHandler(fakeRepository);

        // Act
        var groups = await sut.HandleAsync(A.Dummy<GetAllGroupsCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.NotEmpty(groups);
    }

    [Fact]
    public async Task When_Dont_Has_Groups_Should_Return_An_Empty_List()
    {
        // Arrange
        var fakeRepository = A.Fake<IGroupRepository>();
        A.CallTo(() => fakeRepository.GetAllAsync(TestContext.Current.CancellationToken))
            .Returns(Enumerable.Empty<Group>().ToList());

        var sut = new GetAllGroupsHandler(fakeRepository);

        // Act
        var groups = await sut.HandleAsync(A.Dummy<GetAllGroupsCommand>(), TestContext.Current.CancellationToken);

        // Assert
        Assert.Empty(groups);
    }
}
