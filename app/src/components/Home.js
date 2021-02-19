import DiscussionsList from './DiscussionsList';
import Pagination from '@material-ui/lab/Pagination';
import useFetchDiscussionsPage from '../hooks/useFetchDiscussionsPage';
import { useState } from 'react';


const Home = () => {
    const [pageNumber, setPageNumber] = useState(1);
    const { data: discussions, isLoading, error } = useFetchDiscussionsPage(pageNumber, 2);

    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Loading...</div>}
            <DiscussionsList discussions={discussions}></DiscussionsList>
            <Pagination count={2} variant="outlined" color="primary"
                onChange={(_, page) => { console.log(`changed to ${page}`); setPageNumber(page) }} />
        </div>

    );
}

export default Home;