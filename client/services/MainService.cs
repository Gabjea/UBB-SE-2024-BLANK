using client.repositories;

namespace client.services
{
    class MainService
    {
        public LocationService LocationService { get; }

        public PostArchivedService PostArchivedService { get; }

        public PostReportedService PostReportedService { get; }

        public PostSavedService PostSavedService { get; }

        public PostsService PostsService { get; }

        public UserService UserService { get; }
        public MainService()
        {
            PostArchivedRepository postArchivedRepository = new PostArchivedRepository();
            PostSavedRepository postSavedRepository = new PostSavedRepository();
            PostReportedRepository postReportedRepository = new PostReportedRepository();
            LocationRepository locationRepository = new LocationRepository();
            PostsRepository postsRepository = new PostsRepository();
            UserRepository userRepository = new UserRepository();

            LocationService = new LocationService(locationRepository);
            PostArchivedService = new PostArchivedService(postArchivedRepository);
            PostReportedService = new PostReportedService(postReportedRepository);
            PostSavedService = new PostSavedService(postSavedRepository);
            PostsService = new PostsService(postsRepository);
            UserService = new UserService(userRepository);
        }
    }
}
