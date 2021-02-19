const DiscussionsList = ({ discussions }) => {
    return (
        <div>
            {discussions?.map((discussion) => (
                <div key={discussion.discussionId}>
                    <h2>{discussion.title}</h2>
                    <p>{discussion.description}</p>
                </div>
            ))}
        </div>
    );
}

export default DiscussionsList;