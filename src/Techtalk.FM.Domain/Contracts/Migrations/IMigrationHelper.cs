namespace Techtalk.FM.Domain.Contracts.Migrations
{
    public interface IMigrationHelper
    {
        void RunMigrationUp();

        void RunMigrationDown();
    }
}
