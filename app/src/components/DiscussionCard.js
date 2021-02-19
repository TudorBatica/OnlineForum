const DiscussionCard = ({ discussion }) => {
    console.log()
    return(
        <div>
            <h2>
                {discussion?.title}
            </h2>
            <p>
                {discussion?.description}
            </p>
            <p>
                {discussion?.datetime}
            </p>
        </div>
    );
}

export default DiscussionCard;