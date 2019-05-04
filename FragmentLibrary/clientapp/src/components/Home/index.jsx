import React from 'react';
import { Loader, Header, Grid, Card, Image } from 'semantic-ui-react';
import './home.scss';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tiles: [],
            isLoading: true,
        }
    }

    componentDidMount(){
        setTimeout(() => {
            this.setState({
                isLoading: false,
                tiles: [
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A1",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    },
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A2",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    },
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A3",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    },
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A4",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    },
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A5",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    },
                    {
                        id:"1-asdasda-asdas-adaasda",
                        name:"A6",
                        frontScanWithoutBackground:{
                            smallImageId: "Image ID"
                        }
                    }
                ]});
        }, 1500);
    }

    render() {
        const {
            tiles,
            isLoading
        } = this.state;
        
        return (
            <div className="home">
                <Header as="h1">
                    Digitalizações
                </Header>
                <Loader indeterminate active={isLoading} >A carregar...</Loader>
                {/* <Grid container relaxed columns={3}> */}
                <Card.Group stackable itemsPerRow={6}>
                {
                    tiles.map(tile=> (
                        <Card as="a" href={`/tile/${tile.id}`}> 
                            <Image src='https://react.semantic-ui.com/images/wireframe/image.png' />
                            <Card.Content>
                            <Card.Header>tile.name</Card.Header>
                            {/* <Card.Meta>
                                <span className='date'>Joined in 2015</span>
                            </Card.Meta>
                            <Card.Description>Matthew is a musician living in Nashville.</Card.Description>
                            </Card.Content>
                            <Card.Content extra>
                            <a>
                                <Icon name='user' />
                                22 Friends
                            </a>*/}
                            </Card.Content> 
                        </Card>
                    ))
                }
                </Card.Group>
                {/* </Grid> */}
            </div>
        );
    }
}

export default Home;