using System.Text.Json;
using Treenity_AI_Scraper.Models.Runtime;
using Treenity_AI_Scraper.Models.Treenity;

namespace Treenity_AI_Scraper.Extensions.API
{
    internal static class Gateway
    {
        static long GetTimeStamp()
        {
            return Convert.ToInt64((DateTime.Now - DateTime.UnixEpoch).TotalMilliseconds);
        }
        static FormUrlEncodedContent Encrypt(this Entity entity,Dictionary<string,object>? payload = null)
        {
            if(payload == null)
            {
                payload = new Dictionary<string, object>
                {
                    ["dateFormate"] = GetTimeStamp()
                };
            }
            else
            {
                payload.Add("dateFormate", GetTimeStamp());
            }
            string secret =  JsonSerializer.Serialize(payload);
            var post = new Dictionary<string, string>
            {
                ["secretStr"] = entity.TreenityCryptoProvider.yxyz(secret, "6"),
                ["date"] = GetTimeStamp().ToString()
            };
            return new FormUrlEncodedContent(post);
        }
        public static async Task<HttpResponseMessage> knowledgePlaned(this Entity entity,string courseId,string classId)
        {
            var payload = new Dictionary<string, object>
            {
                ["courseId"] = courseId,
                ["classId"] = classId
            };
            var ctx = Encrypt(entity,payload);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/knowledgePlaned", ctx);
        }
        public static async Task<KnowledgePlaned?> KnowledgePlaned(this Entity entity, string courseId, string classId)
        {
            var payload = new Dictionary<string, object>
            {
                ["courseId"] = courseId,
                ["classId"] = classId
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.TreenityPostAsJsonAsync<KnowledgePlaned>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/knowledgePoint/knowledgePlaned", ctx);
        }
        public static async Task<HttpResponseMessage> startCreatePagerByKenoledgeId(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/startCreatePagerByKenoledgeId", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentPaperQuestionList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/findStudentPaperQuestionList/web", ctx);
        }
        public static async Task<HttpResponseMessage> getPaperId(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/getPaperId/web", ctx);
        }
        public static async Task<HttpResponseMessage> findStudentPaperQuestionDetailByID(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/findStudentPaperQuestionDetailByID/web", ctx);
        }
        public static async Task<HttpResponseMessage> saveStudentPageAnswer(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/saveStudentPageAnswer/web", ctx);
        }
        public static async Task<HttpResponseMessage> startStudentPaper(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/startStudentPaper", ctx);
        }
        public static async Task<HttpResponseMessage> findByStudentMaster(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/findByStudentMaster", ctx);
        }
        public static async Task<HttpResponseMessage> findByStudentKnowledgeMasterList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/findByStudentKnowledgeMasterList", ctx);
        }
        public static async Task<HttpResponseMessage> getAccessKey(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/stuApi/resource/getAccessKey", ctx);
        }
        public static async Task<HttpResponseMessage> getStudentDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/studyRecord/getStudentDetail", ctx);
        }
        public static async Task<HttpResponseMessage> getKnowledgeVideoTime(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/studyRecord/getKnowledgeVideoTime", ctx);
        }
        public static async Task<HttpResponseMessage> setReport(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/studyRecord/report", ctx);
        }
        public static async Task<HttpResponseMessage> getNewExamLoginUserInfo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/ulogin/getLoginUserInfo", ctx);
        }
        public static async Task<HttpResponseMessage> getOverview(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/get_overview", ctx);
        }
        public static async Task<HttpResponseMessage> compareCourseBase(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/compare_course_base", ctx);
        }
        public static async Task<HttpResponseMessage> compareTwoCourse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/compare_two_course", ctx);
        }
        public static async Task<HttpResponseMessage> getKnCourseSimi(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/get_kn_course_simi", ctx);
        }
        public static async Task<HttpResponseMessage> searchCourse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/search_course", ctx);
        }
        public static async Task<HttpResponseMessage> searchKnowledge(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/search_knowledge", ctx);
        }
        public static async Task<HttpResponseMessage> getVideoById(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://newbase.zhihuishu.com/video/initVideoNew", ctx);
        }
        public static async Task<HttpResponseMessage> getEntities(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/get_entities", ctx);
        }
        public static async Task<HttpResponseMessage> updateEntities(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/update_entities_annotation", ctx);
        }
        public static async Task<HttpResponseMessage> getvideotopic(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://http://ai-demo.zhihuishu.com/kg-base/get_video_with_topic", ctx);
        }
        public static async Task<HttpResponseMessage> getMapList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/list-knowledge-map", ctx);
        }
        public static async Task<HttpResponseMessage> createdMapList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/create-map", ctx);
        }
        public static async Task<HttpResponseMessage> delMapList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/del-map", ctx);
        }
        public static async Task<HttpResponseMessage> listContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/list-content", ctx);
        }
        public static async Task<HttpResponseMessage> listMapContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/list-map-content", ctx);
        }
        public static async Task<HttpResponseMessage> deleteMapContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/delete-map-content", ctx);
        }
        public static async Task<HttpResponseMessage> listMapTreeContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/list-map-tree-content", ctx);
        }
        public static async Task<HttpResponseMessage> getContentChapterTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/get-content-chapter-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getContentBook(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/get-content-book", ctx);
        }
        public static async Task<HttpResponseMessage> getContentConcept(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/get-content-concept", ctx);
        }
        public static async Task<HttpResponseMessage> listmapContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/list-map-content", ctx);
        }
        public static async Task<HttpResponseMessage> getMapTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> saveMapTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/save-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getContentCourse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/get-content-course", ctx);
        }
        public static async Task<HttpResponseMessage> addMapContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/add-map-content", ctx);
        }
        public static async Task<HttpResponseMessage> getContentCideo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/content/get-content-course-video-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getMapTreeNodeDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-map-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> updateTreeNodeDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/add-update-map-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getlistCourseDomain(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-course-domain", ctx);
        }
        public static async Task<HttpResponseMessage> getlistTreeNodeChapterRecommend(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-chapter-recommend", ctx);
        }
        public static async Task<HttpResponseMessage> searchListTreeNodeChapter(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-chapter-search", ctx);
        }
        public static async Task<HttpResponseMessage> getlistTreeNodeChapterSource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-chapter-source", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeNodeChapterContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-chapter-content", ctx);
        }
        public static async Task<HttpResponseMessage> markTreeNodeChapterContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/mark-tree-node-chapter-content", ctx);
        }
        public static async Task<HttpResponseMessage> operTreeNodeChapter(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/oper-tree-node-chapter", ctx);
        }
        public static async Task<HttpResponseMessage> getNotRecommendSource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-video-not-recommend-source", ctx);
        }
        public static async Task<HttpResponseMessage> getNotRecommendChapter(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-chapter-not-recommend-source", ctx);
        }
        public static async Task<HttpResponseMessage> sortTreeNodeChapter(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/sort-tree-node-chapter", ctx);
        }
        public static async Task<HttpResponseMessage> listTreeNodeVideoRecommende(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-video-recommend", ctx);
        }
        public static async Task<HttpResponseMessage> listTreeNodeVideoSearch(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-video-search", ctx);
        }
        public static async Task<HttpResponseMessage> feedbackTreeNodeVideo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/feedback-tree-node-video", ctx);
        }
        public static async Task<HttpResponseMessage> listTreeNodeVideoSourse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-video-source", ctx);
        }
        public static async Task<HttpResponseMessage> operTreeNodeVideo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/oper-tree-node-video", ctx);
        }
        public static async Task<HttpResponseMessage> sortTreeNodeVideo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/sort-tree-node-video", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeNodeVideoDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-video-detail", ctx);
        }
        public static async Task<HttpResponseMessage> otherResourcesList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-tree-node-resources", ctx);
        }
        public static async Task<HttpResponseMessage> uploadOtherResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/upload-tree-node-resources", ctx);
        }
        public static async Task<HttpResponseMessage> delOtherResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/del-tree-node-resources", ctx);
        }
        public static async Task<HttpResponseMessage> localResourcesList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-tree-node-video-resources", ctx);
        }
        public static async Task<HttpResponseMessage> uploadLocalResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/upload-tree-node-video-resources", ctx);
        }
        public static async Task<HttpResponseMessage> delLocalResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/del-tree-node-video-resources", ctx);
        }
        public static async Task<HttpResponseMessage> editLocalResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/edit-tree-node-video-resources", ctx);
        }
        public static async Task<HttpResponseMessage> sortLocalResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/sort-tree-node-video-resources", ctx);
        }
        public static async Task<HttpResponseMessage> nodeAttrList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-map-tree-attr-node", ctx);
        }
        public static async Task<HttpResponseMessage> showMapTreeNode(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/show-map-tree-node", ctx);
        }
        public static async Task<HttpResponseMessage> sortAttrNode(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/sort-map-tree-attr-node", ctx);
        }
        public static async Task<HttpResponseMessage> loginReport(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/login-report", ctx);
        }
        public static async Task<HttpResponseMessage> queryMapAuthority(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/get-map-user-auth", ctx);
        }
        public static async Task<HttpResponseMessage> addMapUser(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/add-map-user", ctx);
        }
        public static async Task<HttpResponseMessage> searchMapUser(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/search-map-user", ctx);
        }
        public static async Task<HttpResponseMessage> editMapUserRole(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/edit-map-user-role", ctx);
        }
        public static async Task<HttpResponseMessage> delMapUser(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/del-map-user", ctx);
        }
        public static async Task<HttpResponseMessage> agreeMapUser(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/agree-map-user", ctx);
        }
        public static async Task<HttpResponseMessage> getMapUserAuth(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/get-map-user-auth", ctx);
        }
        public static async Task<HttpResponseMessage> getMapInvitor(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/get-map-invitor", ctx);
        }
        public static async Task<HttpResponseMessage> listMapUser(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/user/list-map-user", ctx);
        }
        public static async Task<HttpResponseMessage> publishMap(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/publish-map", ctx);
        }
        public static async Task<HttpResponseMessage> cancelPublishMap(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/cancel-publish-map", ctx);
        }
        public static async Task<HttpResponseMessage> editMap(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/edit-map", ctx);
        }
        public static async Task<HttpResponseMessage> getMapDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/get-map-detail", ctx);
        }
        public static async Task<HttpResponseMessage> submitBrowseHistory(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprecord/submit-browse", ctx);
        }
        public static async Task<HttpResponseMessage> handleCollect(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprecord/submit-like", ctx);
        }
        public static async Task<HttpResponseMessage> queryCollectState(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprecord/is-like", ctx);
        }
        public static async Task<HttpResponseMessage> queryListLike(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprecord/list-like", ctx);
        }
        public static async Task<HttpResponseMessage> queryListBrowse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maprecord/list-browse", ctx);
        }
        public static async Task<HttpResponseMessage> listTreeNodeRecommend(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-detail-recommend", ctx);
        }
        public static async Task<HttpResponseMessage> getBookRelTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-detail-chapter-directory", ctx);
        }
        public static async Task<HttpResponseMessage> getBookVideoTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-detail-video-directory", ctx);
        }
        public static async Task<HttpResponseMessage> listTreeNodeDetailSearch(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-detail-chapter-search", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeNodeDetailContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-detail-chapter-content", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeNodeVideoDetailContent(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-detail-video-content", ctx);
        }
        public static async Task<HttpResponseMessage> notRecommendDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/not-recommend-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> cancelMergeRecommend(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/cancel-merge-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> mergeRecommend(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/merge-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> queryFocusPoint(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/get-map-focus", ctx);
        }
        public static async Task<HttpResponseMessage> queryCoreList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-map-tree-core-node", ctx);
        }
        public static async Task<HttpResponseMessage> editRecommend(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/edit-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> sortTreeNodeDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/sort-tree-node-detail", ctx);
        }
        public static async Task<HttpResponseMessage> focusMap(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/focus-map", ctx);
        }
        public static async Task<HttpResponseMessage> mapScaleReport(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/submit-map-scale", ctx);
        }
        public static async Task<HttpResponseMessage> searchVideoTree(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-detail-video-search", ctx);
        }
        public static async Task<HttpResponseMessage> getThemeList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-map-tree-theme-node", ctx);
        }
        public static async Task<HttpResponseMessage> recommendTrack(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/recommend-track", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeMapData(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> saveTreeMapData(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/save-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getXmindNodeList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-xmind-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeMapDataV2(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/get-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getTreeMapDataV3(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v3/get-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> getChildTreeMapDataV2(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/get-node-child-tree", ctx);
        }
        public static async Task<HttpResponseMessage> saveNewTreeXmindNodeListV2(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/save-xmind-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> saveTreeMapDataV2(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/v2/save-tree-map-tree", ctx);
        }
        public static async Task<HttpResponseMessage> saveXmindNodeList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/save-xmind-node-list", ctx);
        }
        public static async Task<HttpResponseMessage> getNodeList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/list-map-tree-node", ctx);
        }
        public static async Task<HttpResponseMessage> getMapQuestionStats(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-map-question-stats", ctx);
        }
        public static async Task<HttpResponseMessage> getMapTaskQuestionStats(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-map-task-question-stats", ctx);
        }
        public static async Task<HttpResponseMessage> getNodeQuestionStats(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-node-question-stats", ctx);
        }
        public static async Task<HttpResponseMessage> getMyTaskQuestionStats(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-my-task-question-stats", ctx);
        }
        public static async Task<HttpResponseMessage> getMyTaskQuestionCount(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-my-task-question-count", ctx);
        }
        public static async Task<HttpResponseMessage> searchMapQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/search-map-question", ctx);
        }
        public static async Task<HttpResponseMessage> getQuestionDetail1(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-question-detail", ctx);
        }
        public static async Task<HttpResponseMessage> assignTask(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/assign-task", ctx);
        }
        public static async Task<HttpResponseMessage> getTaskQuestionLog(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-task-question-log", ctx);
        }
        public static async Task<HttpResponseMessage> getQuestiontype(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-node-questiontype-stats", ctx);
        }
        public static async Task<HttpResponseMessage> searchNodeQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/search-node-question", ctx);
        }
        public static async Task<HttpResponseMessage> searchTaskQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/search-task-question", ctx);
        }
        public static async Task<HttpResponseMessage> addQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/add-map-question", ctx);
        }
        public static async Task<HttpResponseMessage> editQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/edit-map-question", ctx);
        }
        public static async Task<HttpResponseMessage> examineQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/examine-map-question", ctx);
        }
        public static async Task<HttpResponseMessage> similarityQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/list-map-similarity-question", ctx);
        }
        public static async Task<HttpResponseMessage> getContentWithFont(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/get-content-withFont", ctx);
        }
        public static async Task<HttpResponseMessage> questionParse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/question-parse", ctx);
        }
        public static async Task<HttpResponseMessage> teacherExamQuestionAnalysis(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://question-import.zhihuishu.com/question/word", ctx);
        }
        public static async Task<HttpResponseMessage> batchImportQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/batch_import_question", ctx);
        }
        public static async Task<HttpResponseMessage> deduplicationQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/deduplication-question", ctx);
        }
        public static async Task<HttpResponseMessage> preDeduplicationQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/pre-deduplication-question", ctx);
        }
        public static async Task<HttpResponseMessage> delMapQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/del-map-question", ctx);
        }
        public static async Task<HttpResponseMessage> listNodeQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/list-node-question", ctx);
        }
        public static async Task<HttpResponseMessage> submitNodeQuestion(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/question/submit-node-question-answer", ctx);
        }
        public static async Task<HttpResponseMessage> addNetwork(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/network/add", ctx);
        }
        public static async Task<HttpResponseMessage> editNetwork(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/network/edit", ctx);
        }
        public static async Task<HttpResponseMessage> delNetwork(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/network/del", ctx);
        }
        public static async Task<HttpResponseMessage> queryNetworkList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/network/list", ctx);
        }
        public static async Task<HttpResponseMessage> sortNetworkList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/network/sort", ctx);
        }
        public static async Task<HttpResponseMessage> queryRecommendNetworkList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-network-recommend", ctx);
        }
        public static async Task<HttpResponseMessage> searchRecommendNetworkList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-network-search", ctx);
        }
        public static async Task<HttpResponseMessage> queryNetworkSourceList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-network-source", ctx);
        }
        public static async Task<HttpResponseMessage> operTreeNodeNetwork(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/oper-tree-node-network", ctx);
        }
        public static async Task<HttpResponseMessage> sortTreeNodeNetwork(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/sort-tree-node-network", ctx);
        }
        public static async Task<HttpResponseMessage> notRecommendNetworkSource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-tree-node-network-not-recommend-source", ctx);
        }
        public static async Task<HttpResponseMessage> addAbilitySystemNode(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/ability/addAbilitySystemNode", ctx);
        }
        public static async Task<HttpResponseMessage> editAbilitySystemNode(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/ability/editAbilitySystemNode", ctx);
        }
        public static async Task<HttpResponseMessage> dropAbilitySystemNode(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/ability/dropAbilitySystemNode", ctx);
        }
        public static async Task<HttpResponseMessage> getGraduationList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/talent/getGraduationList", ctx);
        }
        public static async Task<HttpResponseMessage> reticularAtlas(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/ability/getAbilitySystemNode", ctx);
        }
        public static async Task<HttpResponseMessage> getMapDetailV2(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/map/v2/get-map-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getMajorMaplistCourse(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/major/home/listCourse", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseInfo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/course/getCourseInfo", ctx);
        }
        public static async Task<HttpResponseMessage> getFrameInfo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/course/getFrameInfo", ctx);
        }
        public static async Task<HttpResponseMessage> listStudyGoal(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-study-goal", ctx);
        }
        public static async Task<HttpResponseMessage> listWeeks(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/teacherApi/studentObsverva/listWeeks", ctx);
        }
        public static async Task<HttpResponseMessage> studentPersonalAnalysisReport(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/studentPersonalAnalysisReport", ctx);
        }
        public static async Task<HttpResponseMessage> studentKnowledgeMasteryAnalysisDto(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/studentKnowledgeMasteryAnalysisDto", ctx);
        }
        public static async Task<HttpResponseMessage> studyDurationAnalysisDto(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/studyRecord/studyDurationAnalysisDto", ctx);
        }
        public static async Task<HttpResponseMessage> getListStudyGoal(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/list-study-goal", ctx);
        }
        public static async Task<HttpResponseMessage> getPPTDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resourceslab/v2/get-ppt-detail", ctx);
        }
        public static async Task<HttpResponseMessage> listBatch(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-platform-cloud-ebook.zhihuishu.com/knowledge-map/ebook/list-batch", ctx);
        }
        public static async Task<HttpResponseMessage> getChapterCoordinate(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/recommend/get-tree-node-chapter-coordinate", ctx);
        }
        public static async Task<HttpResponseMessage> getListNodeResources(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resources/list-node-resources", ctx);
        }
        public static async Task<HttpResponseMessage> batchBookInfo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-platform-cloud-ebook.zhihuishu.com/knowledge-map/ebook/catalog/book-info/batch", ctx);
        }
        public static async Task<HttpResponseMessage> getResourcesDetail(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resources/get-node-resources-detail", ctx);
        }
        public static async Task<HttpResponseMessage> getPPTPages(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resourceslab/get-ppt-page", ctx);
        }
        public static async Task<HttpResponseMessage> getVideoTimes(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resourceslab/get-video-time", ctx);
        }
        public static async Task<HttpResponseMessage> getNodeAttrList(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/maptree/get-node-attr-list", ctx);
        }
        public static async Task<HttpResponseMessage> findKnowledgeInfo(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://newcourse-api.zhihuishu.com/newcourse/gateway/t/v1/aiResource/findKownledgeInfo", ctx);
        }
        public static async Task<HttpResponseMessage> batchAiNodeResource(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://ai-knowledge-map-platform.zhihuishu.com/knowledgemap/gateway/t/resources/batch-ai-node-resource", ctx);
        }
        public static async Task<HttpResponseMessage> getAilistNodeDesc(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://aiknowledgegraph.zhihuishu.com/aiknowledge/gateway/t/v1/knowlegdePoint/listNodeDesc", ctx);
        }
        public static async Task<HttpResponseMessage> getAIStudentMapMaster(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/run/findByStudentMapMasterRows", ctx);
        }
        public static async Task<HttpResponseMessage> getCourseBaseInfo(this Entity entity,string courseId)
        {
            var payload = new Dictionary<string, object>
            {
                ["courseId"] = courseId
            };
            var ctx = Encrypt(entity,payload);
            return await entity.client!.PostAsync("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/findByStudentCourse", ctx);
        }
        public static async Task<Polymerization?> GetCourseBaseInfo(this Entity entity, string courseId)
        {
            var payload = new Dictionary<string, object>
            {
                ["courseId"] = courseId
            };
            var ctx = Encrypt(entity, payload);
            return await entity.client!.TreenityPostAsJsonAsync<Polymerization>("https://zhihuishu-ai-run-api.zhihuishu.com/run/gateway/t/v1/profession/polymerization/findByStudentCourse", ctx);
        }



        public static async Task<HttpResponseMessage> queryDMBAuthorizeKey(this Entity entity)
        {
            var ctx = Encrypt(entity);
            return await entity.client!.PostAsync("https://dmb-future-learn-center.zhihuishu.com/gateway/f/v1/future/learn/api/rule/get/api/key?relationType=14", ctx);
        }
    }
}
