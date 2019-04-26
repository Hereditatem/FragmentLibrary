using System.Collections.Generic;
using System.Threading.Tasks;
using FragmentLibrary.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace FragmentLibrary.Repository
{
    public class FragmentRepository
    {
        private IMongoCollection<Fragment> _fragmentsCollection;

        public FragmentRepository(MongoClient dbClient)
        {
            var db = dbClient.GetDatabase("fragmentsLibrary");
            _fragmentsCollection = db.GetCollection<Fragment>("fragments");
        }

        public static void Initialize()
        {
            BsonClassMap.RegisterClassMap<Fragment>(fmap =>
            {
                fmap.MapIdMember(f => f.Id);
                fmap.MapMember(f => f.Name);
                fmap.MapMember(f => f.FrontScan);
                fmap.MapMember(f => f.BacktScan);
                fmap.MapMember(f => f.FrontScanWithoutBackground);
                fmap.MapMember(f => f.BackScanWithoutBackground);
                fmap.MapMember(f => f.FrontToBackScanAlignment);
                fmap.MapMember(f => f.FrontToBackWithoutBackgroundScanAlignment);
                fmap.MapMember(f => f.PanelId);
                fmap.MapCreator(f => Fragment.BuildFromRepository(
                                                f.Id,
                                                f.Name,
                                                f.FrontScan,
                                                f.BacktScan,
                                                f.FrontScanWithoutBackground,
                                                f.BackScanWithoutBackground,
                                                f.FrontToBackScanAlignment,
                                                f.FrontToBackWithoutBackgroundScanAlignment,
                                                f.PanelId));
            });

            BsonClassMap.RegisterClassMap<Scan>(smap =>
            {
                smap.MapIdMember(s => s.OriginalImageId);
                smap.MapCreator(s => new Scan(s.OriginalImageId));
            });

            BsonClassMap.RegisterClassMap<ProcessedScan>(psmap =>
            {
                psmap.MapIdMember(s => s.OriginalImageId);
                psmap.MapIdMember(s => s.LargeImageId);
                psmap.MapIdMember(s => s.MediumImageId);
                psmap.MapIdMember(s => s.SmallImageId);
                psmap.MapCreator(s => new ProcessedScan(s.OriginalImageId)
                {
                    LargeImageId = s.LargeImageId,
                    MediumImageId = s.MediumImageId,
                    SmallImageId = s.SmallImageId
                });
            });

            BsonClassMap.RegisterClassMap<FrontToBackScanAlignment>(f2bsa => {
                f2bsa.MapMember(f2b => f2b.Left);
                f2bsa.MapMember(f2b => f2b.Right);
                f2bsa.MapMember(f2b => f2b.Angle);
                f2bsa.MapMember(f2b => f2b.Scale);
            });
        }

        public async Task<Fragment> Add(Fragment fragment)
        {          
            await _fragmentsCollection.InsertOneAsync(fragment);
            return fragment;
        }

        public async Task<Fragment> Find(long id)
        {
            return await _fragmentsCollection.Find(f => f.Id == id).SingleAsync();
        }

        public async Task<ICollection<Fragment>> List()
        {
            return await _fragmentsCollection.Find(FilterDefinition<Fragment>.Empty).ToListAsync();
        }

        public async Task<ICollection<Fragment>> FindPanelFragments(int panelId)
        {
            FilterDefinition<Fragment> filter = new ExpressionFilterDefinition<Fragment>(f => f.PanelId != null && f.PanelId.Value == panelId);
            var resutlts = await _fragmentsCollection.FindAsync<Fragment>(filter);
            return resutlts.ToList();
        }

        public async Task<ICollection<Fragment>> FindUnkownPanelFragments()
        {
            FilterDefinition<Fragment> filter = new ExpressionFilterDefinition<Fragment>(f => f.PanelId == null);
            var resutlts = await _fragmentsCollection.FindAsync<Fragment>(filter);
            return resutlts.ToList();
        }
    }
}
