import DiscussionListTile from './DiscussionListTile';
import { Link } from "react-router-dom";

const DiscussionsList = ({ discussions }) => {
    return (
        <div>
            {discussions?.map((discussion) => (
                <div key={discussion.discussionId}>
                    <Link to={`/discussion/${discussion.discussionId}`}>
                        <DiscussionListTile discussion={discussion} />
                    </Link>
                </div>
            ))}
        </div>
    );
}

export default DiscussionsList;