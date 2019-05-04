import React from 'react';
import { 
    Header,
    Segment,
    Grid,
    Image } from 'semantic-ui-react';

class TileDetails extends React.Component{
    constructor(props){
        super(props);
        this.state={
            id: props.match.params.id,
            name: "",
            frontScan: "",
            backScan: "",
            frontScanWithoutBackground: "",
            backScanWithoutBackground: "",
        };
    }
    
    render(){
        return(
            <div className="tile-details">
                <Header as="h1">
                    {this.state.name}
                </Header>
                <Segment>
                    <Header as="h3">
                        Originais
                    </Header>
                    <Grid>
                        <Grid.Column width={4}>
                            <Image fluid src='https://react.semantic-ui.com/images/wireframe/image.png' />
                        </Grid.Column>
                        <Grid.Column width={4}>
                            <Image fluid src='https://react.semantic-ui.com/images/wireframe/image.png' />
                        </Grid.Column>
                    </Grid>
                </Segment>
                <Segment>
                    <Header as="h3">
                        Com fundo transparente
                    </Header>
                    <Grid>
                        <Grid.Column width={4}>
                            <Image fluid src='https://react.semantic-ui.com/images/wireframe/image.png' />
                        </Grid.Column>
                        <Grid.Column width={4}>
                            <Image fluid src='https://react.semantic-ui.com/images/wireframe/image.png' />
                        </Grid.Column>
                    </Grid>
                </Segment>
            </div>            
        );
    }
}

export default TileDetails;