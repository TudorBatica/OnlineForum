import useFetchDiscussionsPage from '../hooks/useFetchDiscussionsPage';
import DiscussionsList from './DiscussionsList';
import Pagination from '@material-ui/lab/Pagination';

const Home = () => {
    
    const { data: discussions, isLoading, error, responseHeaders } = useFetchDiscussionsPage(1, 2);
    console.log(responseHeaders?.get('Content-Length'));
    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Loading...</div>}
            <DiscussionsList discussions={discussions}></DiscussionsList>
            <Pagination count={3} variant="outlined" color="primary" onChange={(_, page) => console.log(`change to ${page}`)} />
        </div>

    );
}

export default Home;