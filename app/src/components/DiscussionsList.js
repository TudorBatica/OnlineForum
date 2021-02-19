import DiscussionListTile from './DiscussionListTile';
import { Link } from "react-router-dom";

const DiscussionsList = ({ discussions }) => {
    return (
        <div>
            {discussions?.map((discussion) => (
                <div key={discussion.discussionId}>
                    <DiscussionListTile discussion={discussion} />
                </div>
            ))}
        </div>
    );
}

export default DiscussionsList;