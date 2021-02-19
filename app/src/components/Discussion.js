import { useParams } from 'react-router-dom';
import DiscussionCard from '../components/DiscussionCard';
import useFetchDiscusion from "../hooks/useFetchDiscussion";

const Discussion = () => {
    const {id} = useParams();
    const { data: discussion, isLoading, error } = useFetchDiscusion(id);
    
    return (
        <div>
            {error && <div>{error}</div>}
            {isLoading && <div>Loading...</div>}
            <DiscussionCard discussion={discussion} />
        </div>

    );
}

export default Discussion;